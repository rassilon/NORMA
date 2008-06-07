﻿
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

CREATE TABLE Person
(
	personId NUMBER(10,0) NOT NULL,
	firstName NVARCHAR2(64) NOT NULL,
	lastName NVARCHAR2(64) NOT NULL,
	"date" DATE NOT NULL,
	mandatoryUniqueDecimal NUMBER(9,0) CHECK (mandatoryUniqueDecimal BETWEEN 4000 AND 20000) NOT NULL,
	mandatoryUniqueString NCHAR(11) NOT NULL,
	mandatoryUniqueTinyInt NUMBER(3,0) CHECK (mandatoryUniqueTinyInt BETWEEN 0 AND 255) NOT NULL,
	genderCode NCHAR(1) CHECK (genderCode IN (N'M', N'F')) NOT NULL,
	mandatoryNonUniqueTinyInt NUMBER(3,0) CHECK (mandatoryNonUniqueTinyInt BETWEEN 0 AND 255) NOT NULL,
	mandatoryNonUniqueUnconstrainedDecimal NUMBER NOT NULL,
	mandatoryNonUniqueUnconstrainedFloat FLOAT(126) NOT NULL,
	optionalUniqueString NCHAR(11),
	ownsCar NUMBER(10,0) CHECK (ownsCar >= 0),
	optionalUniqueDecimal NUMBER(9,0),
	optionalUniqueTinyInt NUMBER(3,0) CHECK (optionalUniqueTinyInt BETWEEN 0 AND 255),
	wife NUMBER(10,0),
	childPersonBirthOrderNr NUMBER(10,0) CHECK (childPersonBirthOrderNr >= 0 AND childPersonBirthOrderNr >= 1),
	childPersonFatherPerson_id NUMBER(10,0),
	childPersonMotherPerson_id NUMBER(10,0),
	ColorARGB NUMBER(10,0),
	hatTypeStyle NVARCHAR2(256),
	isDead NCHAR(1),
	hasParents NCHAR(1),
	optionalNonUniqueTinyInt NUMBER(3,0) CHECK (optionalNonUniqueTinyInt BETWEEN 0 AND 255),
	valueType1DoesSomethingElseWith NUMBER(10,0),
	deathDate DATE,
	deathCause NVARCHAR2(14) CHECK (deathCause IN (N'natural', N'not so natural')),
	deathNaturalDeathIsFromProstateCancer NCHAR(1),
	deathUnnaturalDeathIsViolent NCHAR(1),
	deathUnnaturalDeathIsBloody NCHAR(1),
	CONSTRAINT Person_PK PRIMARY KEY(personId),
	CONSTRAINT Person_UC1 UNIQUE(firstName, "date"),
	CONSTRAINT Person_UC2 UNIQUE(lastName, "date"),
	CONSTRAINT Person_UC3 UNIQUE(optionalUniqueString),
	CONSTRAINT Person_UC4 UNIQUE(wife),
	CONSTRAINT Person_UC5 UNIQUE(ownsCar),
	CONSTRAINT Person_UC6 UNIQUE(optionalUniqueDecimal),
	CONSTRAINT Person_UC7 UNIQUE(mandatoryUniqueDecimal),
	CONSTRAINT Person_UC8 UNIQUE(mandatoryUniqueString),
	CONSTRAINT Person_UC9 UNIQUE(optionalUniqueTinyInt),
	CONSTRAINT Person_UC10 UNIQUE(mandatoryUniqueTinyInt),
	CONSTRAINT Person_UC11 UNIQUE(childPersonFatherPerson_id, childPersonBirthOrderNr, childPersonMotherPerson_id),
	CONSTRAINT Person_mandatoryUniqueDecimal_RoleValueConstraint2 CHECK (mandatoryUniqueDecimal BETWEEN 9000 AND 10000),
	CONSTRAINT Person_optionalUniqueDecimal_RoleValueConstraint1 CHECK (optionalUniqueDecimal BETWEEN 100 AND 4000),
	CONSTRAINT Person_Death_MandatoryGroup CHECK (deathCause IS NOT NULL OR deathCause IS NULL AND deathDate IS NULL AND deathNaturalDeathIsFromProstateCancer IS NULL AND deathUnnaturalDeathIsViolent IS NULL AND deathUnnaturalDeathIsBloody IS NULL),
	CONSTRAINT Person_ChildPerson_MandatoryGroup CHECK (childPersonBirthOrderNr IS NOT NULL AND childPersonMotherPerson_id IS NOT NULL AND childPersonFatherPerson_id IS NOT NULL OR childPersonBirthOrderNr IS NULL AND childPersonMotherPerson_id IS NULL AND childPersonFatherPerson_id IS NULL)
);

CREATE TABLE Task
(
	taskId NUMBER(10,0) NOT NULL,
	personId NUMBER(10,0) NOT NULL,
	CONSTRAINT Task_PK PRIMARY KEY(taskId)
);

CREATE TABLE ValueType1
(
	"value" NUMBER(10,0) NOT NULL,
	doesSomethingWithPerson NUMBER(10,0),
	CONSTRAINT ValueType1_PK PRIMARY KEY("value")
);

CREATE TABLE PersonDrivesCar
(
	drivesCar NUMBER(10,0) CHECK (drivesCar >= 0) NOT NULL,
	drivenByPerson NUMBER(10,0) NOT NULL,
	CONSTRAINT PersonDrivesCar_PK PRIMARY KEY(drivesCar, drivenByPerson)
);

CREATE TABLE PersonBoughtCarFromPersonDate
(
	carSold NUMBER(10,0) CHECK (carSold >= 0) NOT NULL,
	buyer NUMBER(10,0) NOT NULL,
	seller NUMBER(10,0) NOT NULL,
	saleDate DATE NOT NULL,
	CONSTRAINT PersonBoughtCarFromPersonDate_PK PRIMARY KEY(buyer, carSold, seller),
	CONSTRAINT PersonBoughtCarFromPersonDate_UC1 UNIQUE(carSold, saleDate, buyer),
	CONSTRAINT PersonBoughtCarFromPersonDate_UC2 UNIQUE(saleDate, seller, carSold)
);

CREATE TABLE Review
(
	car NUMBER(10,0) CHECK (car >= 0) NOT NULL,
	criterion NVARCHAR2(64) NOT NULL,
	nr NUMBER(10,0) CHECK (nr >= 0 AND nr IN (9, 10, 12) OR nr BETWEEN 1 AND 7 OR nr BETWEEN 14 AND 16 OR nr >= 18) NOT NULL,
	CONSTRAINT Review_PK PRIMARY KEY(car, criterion)
);

CREATE TABLE PersonHasNickName
(
	nickName NVARCHAR2(64) NOT NULL,
	personId NUMBER(10,0) NOT NULL,
	CONSTRAINT PersonHasNickName_PK PRIMARY KEY(nickName, personId)
);

ALTER TABLE Person ADD CONSTRAINT Person_FK1 FOREIGN KEY (wife)  REFERENCES Person (personId) ;

ALTER TABLE Person ADD CONSTRAINT Person_FK2 FOREIGN KEY (valueType1DoesSomethingElseWith)  REFERENCES ValueType1 ("value") ;

ALTER TABLE Person ADD CONSTRAINT Person_FK3 FOREIGN KEY (childPersonFatherPerson_id)  REFERENCES Person (personId) ;

ALTER TABLE Person ADD CONSTRAINT Person_FK4 FOREIGN KEY (childPersonMotherPerson_id)  REFERENCES Person (personId) ;

ALTER TABLE Task ADD CONSTRAINT Task_FK FOREIGN KEY (personId)  REFERENCES Person (personId) ;

ALTER TABLE ValueType1 ADD CONSTRAINT ValueType1_FK FOREIGN KEY (doesSomethingWithPerson)  REFERENCES Person (personId) ;

ALTER TABLE PersonDrivesCar ADD CONSTRAINT PersonDrivesCar_FK FOREIGN KEY (drivenByPerson)  REFERENCES Person (personId) ;

ALTER TABLE PersonBoughtCarFromPersonDate ADD CONSTRAINT PersonBoughtCarFromPersonDate_FK1 FOREIGN KEY (buyer)  REFERENCES Person (personId) ;

ALTER TABLE PersonBoughtCarFromPersonDate ADD CONSTRAINT PersonBoughtCarFromPersonDate_FK2 FOREIGN KEY (seller)  REFERENCES Person (personId) ;

ALTER TABLE PersonHasNickName ADD CONSTRAINT PersonHasNickName_FK FOREIGN KEY (personId)  REFERENCES Person (personId) ;

COMMIT WORK;
