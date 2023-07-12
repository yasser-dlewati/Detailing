-- car table
CREATE TABLE `detailing`.`car` (
  `Id` INT NOT NULL AUTO_INCREMENT,
  `Manufacturer` NVARCHAR(100) NOT NULL,
  `Model` VARCHAR(100) NOT NULL,
  `Year` VARCHAR(45) NOT NULL,
  `Color` VARCHAR(45) NOT NULL,
  `OwnerId` INT NOT NULL,
  `LastDetailingDate` DATETIME NULL,
  PRIMARY KEY (`Id`),
  UNIQUE INDEX `Id_UNIQUE` (`Id` ASC) VISIBLE);

-- customer table
CREATE TABLE `detailing`.`Customer` (
  `Id` INT NOT NULL AUTO_INCREMENT,
  `FirstName` NVARCHAR(100) NOT NULL,
  `MiddleName` NVARCHAR(100) NULL,
  `LastName` NVARCHAR(100) NOT NULL,
  `PreferredName` NVARCHAR(100) NULL,
  `DOB` DATETIME NULL,
  `Mobile` VARCHAR(45) NOT NULL,
  `AddressId` INT NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE INDEX `Id_UNIQUE` (`Id` ASC) VISIBLE);

-- Detailer table
CREATE TABLE `detailing`.`Detailer` (
  `Id` INT NOT NULL AUTO_INCREMENT,
  `FirstName` NVARCHAR(100) NOT NULL,
  `MiddleName` NVARCHAR(100) NULL,
  `LastName` NVARCHAR(100) NOT NULL,
  `PreferredName` NVARCHAR(100) NULL,
  `DOB` DATETIME NULL,
  `Mobile` VARCHAR(45) NOT NULL,
  `AddressId` INT NOT NULL,
  `HasBusiness` BOOL,
  `DetailsExterior` BOOL,
  `DetailsInterior` BOOL,
  `IsMobile` BOOL,
  PRIMARY KEY (`Id`),
  UNIQUE INDEX `Id_UNIQUE` (`Id` ASC) VISIBLE);

-- business table
CREATE TABLE `detailing`.`Business` (
  `Id` INT NOT NULL AUTO_INCREMENT,
  `OwnerId` INT NOT NULL,
  `AddressId` INT NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE INDEX `Id_UNIQUE` (`Id` ASC) VISIBLE);

-- address table
CREATE TABLE `detailing`.`Address` (
  `Id` INT NOT NULL AUTO_INCREMENT,
  `Line1` NVARCHAR(255) NOT NULL,
  `Line2` NVARCHAR(255) NULL,
  `ZipCode` VARCHAR(10) NOT NULL,
  `City` NVARCHAR(100) NOT NULL,
  `Country` VARCHAR(100) NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE INDEX `Id_UNIQUE` (`Id` ASC) VISIBLE);

-- owner-cars table
CREATE TABLE `detailing`.`OwnerCars` (
  `CarId` INT NOT NULL,
  `OwnerId` INT NOT NULL,
  PRIMARY KEY (`CarId`, `OwnerId`));

-- business-detailers table
CREATE TABLE `detailing`.`BusinessDetailers` (
  `BusinessId` INT NOT NULL,
  `DetailerId` INT NOT NULL,
  PRIMARY KEY (`BusinessId`, `DetailerId`));
  