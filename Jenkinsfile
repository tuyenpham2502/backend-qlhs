pipeline {
    agent any
    environment {
         EVN_NAME='development'
    }
    options {
        timeout(time: 1, unit: 'HOURS')
    }
    stages {
        stage('SETTING UP PERMISSIONS PHASE') {
        //   when {
        //   	branch 'develop'
        //   }
            steps {
                echo 'Setting up permission ...'
                sh 'chmod +x ./build.sh'
                sh 'ls -l'
            }
        }
        stage('BUILDING PHASE') {
        //   when {
        //   	branch 'develop'
        //   }
            steps {
                echo 'Building ...'
                sh 'pwd'
                sh 'ls'
                sh './build.sh -e $EVN_NAME -v $GIT_COMMIT'
            }
        }
         stage('CHECKING DOCKER PHASE') {
        //    when {
        //   	branch 'develop'
        //   }
            steps {
                echo 'Checking docker ...'
                sh '/usr/bin/docker ps'
            }
        }
    }
}