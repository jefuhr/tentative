#! /bin/bash

#installs dependences for tentive 
SERVER="10.192.243.172"
USERNAME="jacob"
PASSWORD="password" 
TEST="Completed"
mkdir -v -p $PWD/deploy
echo 1 for Development, 2 for QA, 3 for Production
read VERISON
echo 1 for Client, 2 for Backend
read WHAT


if [[ $VERISON -eq 1 ]] 
then
	if [[ $WHAT -eq 2 ]]
	then
		scp -r $USERNAME@$SERVER:tentative/deployment/dependences $PWD/deploy

		#apt update 
		dpkg -i $PWD/deploy/dependences/apacheds-2.0.0.AM26-amd64.deb
		dpkg -i $PWD/deploy/dependences/php_7.2+60ubuntu1_all.deb
		dpkg -i $PWD/deploy/dependences/mysql-apt-config_0.8.17-1_all.deb
		dpkg -i $PWD/deploy/dependences/rabbitmq-server_3.8.14-1_all.deb
		
	elif [[ $WHAT -eq 1 ]]
	then 
		git clone https://github.com/jefuhr/tentative.git ./$PWD/deploy
		

	fi
fi
if [[ $VERISON -eq 2 ]] 
then
	if [[ $WHAT -eq 2 ]]
	then
		scp -r $USERNAME@$SERVER:tentative/deployment/dependences $PWD/deploy

		#apt update 
		dpkg -i $PWD/deploy/dependences/apacheds-2.0.0.AM26-amd64.deb
		dpkg -i $PWD/deploy/dependences/php_7.2+60ubuntu1_all.deb
		dpkg -i $PWD/deploy/dependences/mysql-apt-config_0.8.17-1_all.deb
		dpkg -i $PWD/deploy/dependences/rabbitmq-server_3.8.14-1_all.deb
		
	elif [[ $WHAT -eq 1 ]]
	then 
		git clone https://github.com/jefuhr/tentative.git ./$PWD/deploy
		

	fi
fi
if [[ $VERISON -eq 3 ]] 
then
	if [[ $WHAT -eq 2 ]]
	then
		scp -r $USERNAME@$SERVER:tentative/deployment/dependences $PWD/deploy

		#apt update 
		dpkg -i $PWD/deploy/dependences/apacheds-2.0.0.AM26-amd64.deb
		dpkg -i $PWD/deploy/dependences/php_7.2+60ubuntu1_all.deb
		dpkg -i $PWD/deploy/dependences/mysql-apt-config_0.8.17-1_all.deb
		dpkg -i $PWD/deploy/dependences/rabbitmq-server_3.8.14-1_all.deb
		
	elif [[ $WHAT -eq 1 ]]
	then 
		git clone https://github.com/jefuhr/tentative.git ./$PWD/deploy
		

	fi
fi
echo "$TEST"
