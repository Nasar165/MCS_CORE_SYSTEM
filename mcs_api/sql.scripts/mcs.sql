DROP DATABASE IF EXISTS testdb;
CREATE DATABASE goldenmonkey CHARACTER SET utf8 COLLATE utf8_unicode_ci;

CREATE USER userAcc WITH
    LOGIN
    NOSUPERUSER
    NOCREATEDB
    NOCREATEROLE
    INHERIT
    NOREPLICATION
    CONNECTION LIMIT -1
    PASSWORD ‘Nasar165’;


DROP TABLE IF EXISTS property CASCADE;
DROP TABLE IF EXISTS contact CASCADE;
DROP TABLE IF EXISTS address CASCADE;
DROP TABLE IF EXISTS facilities CASCADE;
DROP TABLE IF EXISTS amenities CASCADE;
DROP TABLE IF EXISTS project CASCADE;
DROP TABLE IF EXISTS unit CASCADE;
DROP TABLE IF EXISTS property_contact CASCADE;
DROP TABLE IF EXISTS property_amnities CASCADE;
DROP TABLE IF EXISTS property_facilities CASCADE;
DROP TABLE IF EXISTS images CASCADE;
DROP TABLE IF EXISTS facilities_property CASCADE;
DROP TABLE IF EXISTS amnities_property CASCADE;
DROP TABLE IF EXISTS district CASCADE;
DROP TABLE IF EXISTS unit_type CASCADE;
DROP TABLE IF EXISTS job_position CASCADE;
DROP TABLE IF EXISTS zooning CASCADE;
DROP TABLE IF EXISTS land CASCADE;
DROP TABLE IF EXISTS property_contact CASCADE;
DROP TABLE IF EXISTS benefits CASCADE;
DROP TABLE IF EXISTS benefits_property CASCADE;
DROP TABLE IF EXISTS title_deed CASCADE;
DROP TABLE IF EXISTS core_system CASCADE;
DROP TABLE IF EXISTS exceptions CASCADE;
DROP TABLE IF EXISTS system_contact CASCADE;
DROP TABLE IF EXISTS status CASCADE;
DROP TABLE IF EXISTS ownership_type CASCADE;

create table district(
    district_id SERIAL not null primary key,
    name VARCHAR(200) not null default 'district name not set',
    sub_district_id int not null default 1,
    foreign key (sub_district_id) references district(district_id)
);

insert into district (name) values('district or sub district not set');
insert into district (name) values('Kathu');
insert into district (name,sub_district_id) values('patong',2);

create table address (
    address_id SERIAL not null primary key,
    street_nr VARCHAR(10) not null default 'nr not set',
    street_name VARCHAR (250) not null default 'street name not set',
    district_id int not null default 1,
    sub_district_id int not null default 1,
    zip int not null default 0,
    Foreign key (district_id) references district(district_id),
    Foreign key (sub_district_id) references district(district_id)
);

Insert into address (zip) values(8300);


create table property(
    ref_id SERIAL primary key not null,
    title VARCHAR(200) not null default 'Title Not set',
    description VARCHAR(2500) not null,
    rating int not null default 0,
    website BOOLEAN not null default '0',
    price float not null default 0,
    vr_image VARCHAR(2500) not null default 'Not defined',
    parent_ref_id int,
    address_id int not null default 1,
    created_at timestamp not null DEFAULT now(),
main_image VARCHAR(2000) not null default 'primary image not set',
    foreign key (parent_ref_id) references property(ref_id),
    foreign key (address_id) references address(address_id) 
);

create table unit_type(
    unit_type_id SERIAL primary key not null,
    name VARCHAR(80) not null default 'Name not set'
);
insert into unit_type (name) values('Property type not defined');
insert into unit_type (name) values('Villa');
insert into unit_type (name) values('Apartment');
insert into unit_type (name) values('Town House');
insert into unit_type (name) values('Studio');

Create table ownership_type(
    Ownership_type_id SERIAL primary key not null,
    name VARCHAR(80) not null
);

Insert into ownership_type (name) values('Leasehold');
Insert into ownership_type(name) values('Freehold');

create table unit(
    ref_id int not null,
    unit_id VARCHAR(10) not null,
    resale BOOLEAN not null default '0',
    unit_type_id int not null default 1,
    building VARCHAR(20) not null default 'no building',
    bedroom int not null default 1,
    bathroom int not null default 1,
    landsize float not null default 0,
    ownership_type_id int not null,
    size float not null,
    primary key(ref_id,unit_id),
    foreign key (ref_id) references property(ref_id),
foreign key (ownership_type_id ) references ownership_type (ownership_type_id ),
    foreign key (unit_type_id) references unit_type(unit_type_id)
);


create table status(
    status_id SERIAL not null primary key,
    name VARCHAR(250) not null
);

insert into status (name) values ('Off-Plan');
insert into status (name) values ('Completed');
insert into status (name) values ('Under Construction');


create table project (
    ref_id SERIAL not null,
    project_id VARCHAR(5) not null,
    name VARCHAR(250),
    buildings int not null default 0,
    units int not null default 0,
    status_id int not null default 1,
    primary key(ref_id,project_id),
    foreign key (ref_id) references property(ref_id),
    foreign key (status_id) references status(status_id)
);

create table title_deed(
    title_deed_id SERIAL not null primary key,
    name VARCHAR(100) not null default 'title deed not defined'
);

insert into title_deed(name) values('title deed not defined');
insert into title_deed(name) values('Chanote');

create table zooning(
    zooning_id SERIAL not null primary key,
    name VARCHAR(150) not null default 'zone not defained',
    color VARCHAR(50) not null default 'zone color not defined'
);

insert into zooning(name) values('zone not defained');
insert into zooning(name, color) values('residential', 'Yellow');

Create table land (
    ref_id INT not null,
    land_id VARCHAR(10) not null,
    size float not null,
    title_deed_id int not null default 1,
    zooning_id int not null default 1,
    primary key(ref_id,land_id),
    foreign key (ref_id) references property(ref_id),
    foreign key (title_deed_id) references title_deed(title_deed_id),
    foreign key (zooning_id) references zooning(zooning_id)
);

create table images (
    ref_id INT not null,
    image_id SERIAL not null,
    filelocation Text not null default 'path not set',
    primary key(image_id),
    foreign key (ref_id) references property(ref_id),
    constraint image_key unique(ref_id,image_id,filelocation)
);

create table facilities(
    facilities_id SERIAL primary key not null,
    name VARCHAR(200) not null default 'failitie has not been defined'
);
insert into facilities (name) values('Fitness');
insert into facilities (name) values('24/7 security');
insert into facilities (name) values('Car parking');

create table facilities_property(
    ref_id INT not null,
    facilities_id int not null,
    primary key(ref_id, facilities_id),
    foreign key (ref_id) references property(ref_id),
    foreign key (facilities_id) references facilities(facilities_id)
);

create table amenities (
    amnities_id SERIAL primary key not null,
    name VARCHAR(200) not null default 'amenetie has not been defined'
);
insert into amenities (name) values('Kitchen');
insert into amenities (name) values('Sea view');
insert into amenities (name) values('Air condition');

create table amnities_property(
    ref_id INT not null,
    amnities_id int not null,
    primary key(ref_id, amnities_id),
    foreign key (ref_id) references property(ref_id),
    foreign key (amnities_id) references amenities (amnities_id)
);

create table job_position(
    job_position_id SERIAL not null primary key,
    name VARCHAR(250) not null default 'position not defined'
);
insert into job_position (name) values('position has not been set');
insert into job_position (name) values('Private owner');
insert into job_position (name) values('Developer');

create table contact(
    contact_id SERIAL not null primary key,
    name VARCHAR(250) not null default 'name not set',
    email VARCHAR(100) not null default 'email not set',
    telephone VARCHAR(2500) not null default 'telephone not set',
    job_position_id int not null default 1,
created_at timestamp not null DEFAULT now(),
    foreign key (job_position_id) references job_position(job_position_id)
);

Create Table property_contact(
    ref_id INT not null,
    contact_id int not null,
    primary key (ref_id,contact_id),
    foreign key (ref_id) references property(ref_id),
    foreign key (contact_id) references contact(contact_id)
);


Create Table benefits(
    benefits_id SERIAL not null primary key,
    name VARCHAR(250) not null default 'benefit not defined',
    is_active Boolean not null default '0'
);

Create Table benefits_property(
    ref_id INT not null,
    benefits_id int not null,
    primary key(ref_id, benefits_id),
    FOREIGN key (ref_id) references property(ref_id),
    foreign key (benefits_id) references benefits(benefits_id)
);


create table core_system(
    core_system_id SERIAL not null primary key,
    username VARCHAR(250) not null,
    password VARCHAR(250) not null
 );
 

create table exceptions (
    exception_id SERIAL not null primary key,
    core_system_id int not null,
    e_date date not null,
    e_class VARCHAR(250) not null,
    e_method VARCHAR(250) not null,
    e_message VARCHAR(250) not null,
    constraint core_e_key unique (exception_id,core_system_id),
    foreign key (core_system_id) references core_system(core_system_id)
);


GRANT USAGE, SELECT ON ALL SEQUENCES IN SCHEMA public TO nasar;
GRANT ALL PRIVILEGES ON ALL TABLES IN SCHEMA public TO nasar;

