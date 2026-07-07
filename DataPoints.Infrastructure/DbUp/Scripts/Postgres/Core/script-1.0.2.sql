-- ARC-4 / RLS: RLS obrigatório em todas as tabelas do schema core. Modelo A (ver IMPROVEMENTS.md § ARC-4
-- > RLS): a autorização por usuário continua na aplicação (SEC-6); RLS aqui é defesa em profundidade e
-- bloqueia por padrão as roles "anon"/"authenticated" que o PostgREST do Supabase usaria.
-- A role "app_user" já foi criada no pipeline Functions (primeiro a rodar, ver DbUpExtension).

GRANT USAGE ON SCHEMA core TO app_user;
GRANT SELECT, INSERT, UPDATE, DELETE ON ALL TABLES IN SCHEMA core TO app_user;
GRANT USAGE, SELECT ON ALL SEQUENCES IN SCHEMA core TO app_user;

DO
$$
DECLARE
    tbl record;
BEGIN
    FOR tbl IN SELECT schemaname, tablename FROM pg_tables WHERE schemaname = 'core'
    LOOP
        EXECUTE format('ALTER TABLE %I.%I ENABLE ROW LEVEL SECURITY', tbl.schemaname, tbl.tablename);
        EXECUTE format('ALTER TABLE %I.%I FORCE ROW LEVEL SECURITY', tbl.schemaname, tbl.tablename);
        EXECUTE format('DROP POLICY IF EXISTS app_user_full_access ON %I.%I', tbl.schemaname, tbl.tablename);
        EXECUTE format(
            'CREATE POLICY app_user_full_access ON %I.%I FOR ALL TO app_user USING (true) WITH CHECK (true)',
            tbl.schemaname, tbl.tablename);
    END LOOP;
END
$$;

-- RLS na tabela de journal do DbUp deste pipeline (public.migrations_core). Só guarda nome de script +
-- data de execução (nada sensível), mas "public" é exposto por padrão pela Data API do Supabase — sem
-- RLS, anon/authenticated conseguiriam ler/escrever nela via PostgREST.
GRANT SELECT, INSERT ON public.migrations_core TO app_user;
REVOKE UPDATE, DELETE ON public.migrations_core FROM app_user;

ALTER TABLE public.migrations_core ENABLE ROW LEVEL SECURITY;
ALTER TABLE public.migrations_core FORCE ROW LEVEL SECURITY;
DROP POLICY IF EXISTS app_user_insert_select ON public.migrations_core;
CREATE POLICY app_user_insert_select ON public.migrations_core
    FOR ALL TO app_user USING (true) WITH CHECK (true);
