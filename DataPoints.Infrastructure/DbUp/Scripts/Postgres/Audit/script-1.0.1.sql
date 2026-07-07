-- ARC-4 / RLS: schema audit é append-only mesmo para a role da aplicação — UPDATE/DELETE são
-- revogados no nível de GRANT (checado antes de qualquer policy de RLS), então mesmo uma policy
-- "FOR ALL" abaixo não permite alterar/apagar uma linha já escrita.
GRANT USAGE ON SCHEMA audit TO app_user;
GRANT SELECT, INSERT ON ALL TABLES IN SCHEMA audit TO app_user;
GRANT USAGE, SELECT ON ALL SEQUENCES IN SCHEMA audit TO app_user;
REVOKE UPDATE, DELETE ON ALL TABLES IN SCHEMA audit FROM app_user;

DO
$$
DECLARE
    tbl record;
BEGIN
    FOR tbl IN SELECT schemaname, tablename FROM pg_tables WHERE schemaname = 'audit'
    LOOP
        EXECUTE format('ALTER TABLE %I.%I ENABLE ROW LEVEL SECURITY', tbl.schemaname, tbl.tablename);
        EXECUTE format('ALTER TABLE %I.%I FORCE ROW LEVEL SECURITY', tbl.schemaname, tbl.tablename);
        EXECUTE format('DROP POLICY IF EXISTS app_user_insert_select ON %I.%I', tbl.schemaname, tbl.tablename);
        EXECUTE format(
            'CREATE POLICY app_user_insert_select ON %I.%I FOR ALL TO app_user USING (true) WITH CHECK (true)',
            tbl.schemaname, tbl.tablename);
    END LOOP;
END
$$;

-- RLS na tabela de journal do DbUp deste pipeline (public.migrations_audit). Só guarda nome de script +
-- data de execução (nada sensível), mas "public" é exposto por padrão pela Data API do Supabase — sem
-- RLS, anon/authenticated conseguiriam ler/escrever nela via PostgREST.
GRANT SELECT, INSERT ON public.migrations_audit TO app_user;
REVOKE UPDATE, DELETE ON public.migrations_audit FROM app_user;

ALTER TABLE public.migrations_audit ENABLE ROW LEVEL SECURITY;
ALTER TABLE public.migrations_audit FORCE ROW LEVEL SECURITY;
DROP POLICY IF EXISTS app_user_insert_select ON public.migrations_audit;
CREATE POLICY app_user_insert_select ON public.migrations_audit
    FOR ALL TO app_user USING (true) WITH CHECK (true);
