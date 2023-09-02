CREATE TABLE employees (
	id uuid NOT NULL,
	name varchar(100) NOT NULL,
	document varchar(30) NOT NULL,
    birthed_at timestamp NOT NULL,
	created_at timestamp NOT NULL,
	updated_at timestamp NULL,
	CONSTRAINT employees_pk PRIMARY KEY (id),
	CONSTRAINT employees_document_un UNIQUE (document)
);