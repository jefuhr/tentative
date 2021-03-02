-- phpMyAdmin SQL Dump
-- version 4.9.5deb2
-- https://www.phpmyadmin.net/
--
-- Host: localhost:3306
-- Generation Time: Mar 02, 2021 at 06:37 PM
-- Server version: 8.0.23-0ubuntu0.20.04.1
-- PHP Version: 7.4.3

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `it490`
--
CREATE DATABASE IF NOT EXISTS `it490` DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci;
USE `it490`;

-- --------------------------------------------------------

--
-- Table structure for table `currency`
--

CREATE TABLE `currency` (
  `currencyType` int NOT NULL,
  `baseValue` int NOT NULL,
  `currentValue` int NOT NULL,
  `food` int NOT NULL,
  `wood` int NOT NULL,
  `stone` int NOT NULL,
  `leather` int NOT NULL,
  `iron` int NOT NULL,
  `gold` int NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Dumping data for table `currency`
--

INSERT INTO `currency` (`currencyType`, `baseValue`, `currentValue`, `food`, `wood`, `stone`, `leather`, `iron`, `gold`) VALUES
(0, 1, 1, 100, 10, 5, 3, 2, 1),
(1, 10, 10, 1000, 100, 50, 30, 20, 10),
(2, 100, 100, 10000, 1000, 500, 300, 20, 10);

-- --------------------------------------------------------

--
-- Table structure for table `forum`
--

CREATE TABLE `forum` (
  `topicID` int NOT NULL,
  `uuid` int NOT NULL,
  `topic` varchar(255) NOT NULL,
  `message` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- --------------------------------------------------------

--
-- Table structure for table `marketplace`
--

CREATE TABLE `marketplace` (
  `uuid` int NOT NULL,
  `tradeID` int NOT NULL,
  `itemType` set('food','wood','stone','leather','iron','gold') NOT NULL,
  `itemQuant` int NOT NULL,
  `requestType` set('food','wood','stone','leather','iron','gold') NOT NULL,
  `requestQuant` int NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- --------------------------------------------------------

--
-- Table structure for table `playerData`
--

CREATE TABLE `playerData` (
  `uuid` int NOT NULL,
  `password` varchar(255) NOT NULL,
  `username` varchar(255) NOT NULL,
  `food` int NOT NULL DEFAULT '0',
  `wood` int NOT NULL DEFAULT '0',
  `stone` int NOT NULL DEFAULT '0',
  `leather` int NOT NULL DEFAULT '0',
  `iron` int NOT NULL DEFAULT '0',
  `gold` int NOT NULL DEFAULT '0',
  `currency0` int NOT NULL DEFAULT '0',
  `currency1` int NOT NULL DEFAULT '0',
  `currency2` int NOT NULL DEFAULT '0',
  `workerCount` int NOT NULL DEFAULT '0'
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Dumping data for table `playerData`
--

INSERT INTO `playerData` (`uuid`, `password`, `username`, `food`, `wood`, `stone`, `leather`, `iron`, `gold`, `currency0`, `currency1`, `currency2`, `workerCount`) VALUES
(1, 'hashed_password', 'test', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);

--
-- Indexes for dumped tables
--

--
-- Indexes for table `currency`
--
ALTER TABLE `currency`
  ADD PRIMARY KEY (`currencyType`);

--
-- Indexes for table `forum`
--
ALTER TABLE `forum`
  ADD PRIMARY KEY (`topicID`);

--
-- Indexes for table `marketplace`
--
ALTER TABLE `marketplace`
  ADD PRIMARY KEY (`tradeID`);

--
-- Indexes for table `playerData`
--
ALTER TABLE `playerData`
  ADD PRIMARY KEY (`uuid`),
  ADD UNIQUE KEY `username` (`username`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `forum`
--
ALTER TABLE `forum`
  MODIFY `topicID` int NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `marketplace`
--
ALTER TABLE `marketplace`
  MODIFY `tradeID` int NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `playerData`
--
ALTER TABLE `playerData`
  MODIFY `uuid` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
