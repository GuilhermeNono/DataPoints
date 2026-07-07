-- ARC-4 / RLS: cria a role de aplicação aqui (não em Core) porque este é o primeiro pipeline a rodar
-- (RunFunctionsDbUp -> RunMainDbUp -> RunAuditDbUp, ver DbUpExtension) — Core e Audit já podem contar
-- com "app_user" existindo. A senha da role é definida fora deste script (Supabase dashboard / secret
-- manager), nunca versionada.
DO
$$
BEGIN
    IF NOT EXISTS (SELECT FROM pg_roles WHERE rolname = 'app_user') THEN
        CREATE ROLE app_user LOGIN NOSUPERUSER NOBYPASSRLS NOCREATEDB NOCREATEROLE;
    END IF;
END
$$;

-- RLS na tabela de journal do DbUp deste pipeline (public.migrations_functions). Ela só guarda nome de
-- script + data de execução (nada sensível), mas o schema "public" é exposto por padrão pela Data API
-- do Supabase — sem RLS, anon/authenticated conseguiriam ler/escrever nela via PostgREST.
GRANT USAGE ON SCHEMA public TO app_user;
GRANT SELECT, INSERT ON public.migrations_functions TO app_user;
REVOKE UPDATE, DELETE ON public.migrations_functions FROM app_user;

ALTER TABLE public.migrations_functions ENABLE ROW LEVEL SECURITY;
ALTER TABLE public.migrations_functions FORCE ROW LEVEL SECURITY;
DROP POLICY IF EXISTS app_user_insert_select ON public.migrations_functions;
CREATE POLICY app_user_insert_select ON public.migrations_functions
    FOR ALL TO app_user USING (true) WITH CHECK (true);
