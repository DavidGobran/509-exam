CREATE TABLE `Customers` (
  `Username` varchar(255) DEFAULT NULL,
  `AccountBalance` int DEFAULT '0',
  `AccountNumber` int NOT NULL AUTO_INCREMENT,
  `Status` varchar(50) DEFAULT NULL,
  `AccountHolder` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`AccountNumber`),
  KEY `Username` (`Username`),
  CONSTRAINT `customers_ibfk_1` FOREIGN KEY (`Username`) REFERENCES `Users` (`Username`)
);

CREATE TABLE `Users` (
  `Username` varchar(255) NOT NULL,
  `UserType` varchar(50) DEFAULT NULL,
  `PinCode` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`Username`)
);

INSERT INTO `Customers` (`Username`, `AccountBalance`, `AccountNumber`, `Status`, `AccountHolder`) VALUES
('Adnan123', 168962, 0, 'Active', 'Test');

INSERT INTO `Users` (`Username`, `UserType`, `PinCode`) VALUES
('Adnan123', 'Customer', '12345'),
('Javed123', 'Administrator', '12345');
