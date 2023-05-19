--
-- PostgreSQL database dump
--

-- Dumped from database version 15.2
-- Dumped by pg_dump version 15.2

-- Started on 2023-05-18 23:40:29

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- TOC entry 216 (class 1259 OID 35653)
-- Name: Lessons; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Lessons" (
    "Id" integer NOT NULL,
    "Name" text NOT NULL,
    "CourseId" integer NOT NULL
);


ALTER TABLE public."Lessons" OWNER TO postgres;

--
-- TOC entry 215 (class 1259 OID 35652)
-- Name: Lessons_Id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public."Lessons" ALTER COLUMN "Id" ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME public."Lessons_Id_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- TOC entry 218 (class 1259 OID 35661)
-- Name: Themes; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Themes" (
    "Id" integer NOT NULL,
    "Name" text NOT NULL,
    "LessonId" integer NOT NULL
);


ALTER TABLE public."Themes" OWNER TO postgres;

--
-- TOC entry 217 (class 1259 OID 35660)
-- Name: Themes_Id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public."Themes" ALTER COLUMN "Id" ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME public."Themes_Id_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- TOC entry 214 (class 1259 OID 35647)
-- Name: __EFMigrationsHistory; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL
);


ALTER TABLE public."__EFMigrationsHistory" OWNER TO postgres;

--
-- TOC entry 3185 (class 2606 OID 35659)
-- Name: Lessons PK_Lessons; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Lessons"
    ADD CONSTRAINT "PK_Lessons" PRIMARY KEY ("Id");


--
-- TOC entry 3188 (class 2606 OID 35667)
-- Name: Themes PK_Themes; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Themes"
    ADD CONSTRAINT "PK_Themes" PRIMARY KEY ("Id");


--
-- TOC entry 3183 (class 2606 OID 35651)
-- Name: __EFMigrationsHistory PK___EFMigrationsHistory; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."__EFMigrationsHistory"
    ADD CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId");


--
-- TOC entry 3186 (class 1259 OID 35673)
-- Name: IX_Themes_LessonId; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_Themes_LessonId" ON public."Themes" USING btree ("LessonId");


--
-- TOC entry 3189 (class 2606 OID 35668)
-- Name: Themes FK_Themes_Lessons_LessonId; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Themes"
    ADD CONSTRAINT "FK_Themes_Lessons_LessonId" FOREIGN KEY ("LessonId") REFERENCES public."Lessons"("Id") ON DELETE CASCADE;


-- Completed on 2023-05-18 23:40:30

--
-- PostgreSQL database dump complete
--
