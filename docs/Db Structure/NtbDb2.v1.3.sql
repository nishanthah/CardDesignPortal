CREATE DATABASE  IF NOT EXISTS `ntbkidscard` /*!40100 DEFAULT CHARACTER SET utf8 */;
USE `ntbkidscard`;
-- MySQL dump 10.13  Distrib 5.7.12, for Win64 (x86_64)
--
-- Host: localhost    Database: ntbkidscard
-- ------------------------------------------------------
-- Server version	5.7.16-log

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `card_request`
--

DROP TABLE IF EXISTS `card_request`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `card_request` (
  `id` int(11) NOT NULL,
  `card_no` varchar(16) NOT NULL,
  `template_id` int(11) NOT NULL,
  `card_holder_name` varchar(25) NOT NULL,
  `expire_date` datetime NOT NULL,
  PRIMARY KEY (`id`,`card_no`),
  KEY `Fk_card_request_card_template_idx` (`template_id`),
  CONSTRAINT `Fk_card_request_card_template` FOREIGN KEY (`template_id`) REFERENCES `card_template` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `Fk_consumer_id_consumer_credential` FOREIGN KEY (`id`) REFERENCES `user` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `card_request`
--

LOCK TABLES `card_request` WRITE;
/*!40000 ALTER TABLE `card_request` DISABLE KEYS */;
INSERT INTO `card_request` VALUES (1,'1',1,'Damtih','2021-12-16 00:00:00');
/*!40000 ALTER TABLE `card_request` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `card_template`
--

DROP TABLE IF EXISTS `card_template`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `card_template` (
  `id` int(11) NOT NULL,
  `image` blob NOT NULL,
  `is_active` tinyint(4) DEFAULT '0',
  `card_templatecol` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `card_template`
--

LOCK TABLES `card_template` WRITE;
/*!40000 ALTER TABLE `card_template` DISABLE KEYS */;
INSERT INTO `card_template` VALUES (1,'',0,NULL);
/*!40000 ALTER TABLE `card_template` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `user`
--

DROP TABLE IF EXISTS `user`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `user` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `username` varchar(15) NOT NULL,
  `password` varchar(255) NOT NULL,
  `is_active` tinyint(4) NOT NULL DEFAULT '0',
  `no_of_attempt` int(11) DEFAULT NULL,
  `reset_code` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `user`
--

LOCK TABLES `user` WRITE;
/*!40000 ALTER TABLE `user` DISABLE KEYS */;
INSERT INTO `user` VALUES (1,'user1','p1',1,1,2718);
/*!40000 ALTER TABLE `user` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `user_detail`
--

DROP TABLE IF EXISTS `user_detail`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `user_detail` (
  `id` int(11) NOT NULL,
  `first_name` varchar(150) NOT NULL,
  `last_name` varchar(60) DEFAULT NULL,
  `phone_number` varchar(10) DEFAULT NULL,
  `email_address` varchar(150) NOT NULL,
  `mailling_address` varchar(150) NOT NULL,
  `town` varchar(30) DEFAULT NULL,
  `cif` varchar(45) DEFAULT NULL,
  `date_of_birth` datetime NOT NULL,
  `account_branch` varchar(60) NOT NULL,
  `image` blob,
  PRIMARY KEY (`id`),
  CONSTRAINT `Fk_detail_consumer_credential` FOREIGN KEY (`id`) REFERENCES `user` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `user_detail`
--

LOCK TABLES `user_detail` WRITE;
/*!40000 ALTER TABLE `user_detail` DISABLE KEYS */;
INSERT INTO `user_detail` VALUES (1,'Samith',NULL,'07712345','Samith@gmial.com','No 06 Dematagoda',NULL,NULL,'1990-10-02 00:00:00','Colombo',NULL);
/*!40000 ALTER TABLE `user_detail` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2017-01-15 15:20:18
