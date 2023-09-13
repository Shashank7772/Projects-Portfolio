CREATE DATABASE  IF NOT EXISTS `dksupermarket` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `dksupermarket`;
-- MySQL dump 10.13  Distrib 8.0.34, for Win64 (x86_64)
--
-- Host: localhost    Database: dksupermarket
-- ------------------------------------------------------
-- Server version	8.0.34

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `products`
--

DROP TABLE IF EXISTS `products`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `products` (
  `ProductBarcode` bigint NOT NULL,
  `ProductName` varchar(255) NOT NULL,
  `ManufacturingDate` date NOT NULL,
  `BestBefore` int NOT NULL,
  `ExpiryDate` date GENERATED ALWAYS AS ((`ManufacturingDate` + interval `BestBefore` month)) VIRTUAL,
  `PurchasePrice` decimal(10,2) NOT NULL,
  `TaxSlab` decimal(10,2) NOT NULL,
  `Tax` decimal(10,2) GENERATED ALWAYS AS ((`PurchasePrice` * `TaxSlab`)) VIRTUAL,
  `SellingPrice` decimal(10,2) GENERATED ALWAYS AS ((`PurchasePrice` + (`PurchasePrice` * `TaxSlab`))) VIRTUAL,
  PRIMARY KEY (`ProductBarcode`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `products`
--

LOCK TABLES `products` WRITE;
/*!40000 ALTER TABLE `products` DISABLE KEYS */;
INSERT INTO `products` (`ProductBarcode`, `ProductName`, `ManufacturingDate`, `BestBefore`, `PurchasePrice`, `TaxSlab`) VALUES (8901030824968,'Dove Creme Bar (125g)','2023-02-09',30,120.00,0.50),(8901030896354,'CloseUp (2 X 150g)','2023-05-01',24,200.00,0.50),(8901526204465,'Garnier Color naturals (Brown)','2022-12-01',34,200.00,0.18),(8901719121425,'Monacco Classic (10rs)','2023-04-08',7,10.00,0.50),(8901860010432,'Pidilite Flex kwik Glue','2022-09-01',12,50.00,0.18),(8906010500030,'Balaji Masala Masti Chips','2023-07-15',4,8.00,0.18),(8906017233641,'Posh Chilly Flakes','2020-06-01',12,90.00,0.18);
/*!40000 ALTER TABLE `products` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2023-09-13 16:47:03
