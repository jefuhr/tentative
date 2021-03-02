<?php

$hostname = "10.192.229.200";
$username = "root";
$password = "it490password";
$database = //name of database;

($db = mysqli_connect($hostname, $username, $password))
	or die("yall done fucked up");

print "good job honey"; 
?>
