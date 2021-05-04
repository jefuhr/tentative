#! /bin/bash

#installs dependences for tentive 
SERVER="10.192.238.148"
USERNAME="jacob"
PASSWORD="password" 
TEST="recived"


mkdir -v -p $PWD/deploy

#sshpass -p "$PASSWORD" 
scp -r $USERNAME@$SERVER:tentative/deployment/dependences $PWD/deploy

#cd -v $PWD/deploy/dependences

#apt update 
dpkg -i $PWD/deploy/dependences/apacheds-2.0.0.AM26-amd64.deb
dpkg -i $PWD/deploy/dependences/php_7.2+60ubuntu1_all.deb
dpkg -i $PWD/deploy/dependences/mysql-apt-config_0.8.17-1_all.deb
dpkg -i $PWD/deploy/dependences/rabbitmq-server_3.8.14-1_all.deb


echo "$TEST"
