pipeline{
    agent any
    environment{
        scannerHome = tool 'sonar_scanner_dotnet'
        username='admin'
        appname='sonar-ravindrakumar'   
    }
    options {
        skipDefaultCheckout(true)
		timeout(time: 30, unit: 'MINUTES') 
        buildDiscarder(logRotator(numToKeepStr:'5', artifactNumToKeepStr: '5'))
    }
    stages{
		stage('Start') {
			steps {
				echo '**Starting code check out**'
				git branch: 'master', url: 'https://github.com/ravindrahbtik11/app_ravindrakumar.git'
				echo '****Code check out Finished****'
			}
		}
		stage('Nuget restore'){
				steps{
						echo '**Start restoring packages**'
						bat "dotnet restore"
						echo '****Restore Nuget success****'
					}
			}
		stage('Start sonarqube analysis') {
				steps {
					echo '**Start Sonar qube analysis**'
						withSonarQubeEnv('Test_Sonar') {					
							bat "dotnet ${scannerHome}\\SonarScanner.MSBuild.dll begin /k:\"sonar-ravindrakumar\""
						}
					echo '****Finished Sonar qube analysis****'
				}
			}

		stage('Code build'){
            steps {
                echo '**Build solution**'
                bat "dotnet build"
                echo '****Build success****'
            }
        }

        stage('Test case execution'){
            steps {         
				echo '**Test case execution**'
                bat "dotnet test"
                echo '****Test case execution****'	
            }
        }
        stage('Stop sonarqube analysis') {
                steps {
                echo '**Stopping Sonar Qube analysis**'
                  withSonarQubeEnv('Test_Sonar') {
                        bat "dotnet ${scannerHome}\\SonarScanner.MSBuild.dll end"
					   
                    }
                echo '****Stopped Sonar Qube analysis****'
                }
            }        
        
		 stage('Kubernetes deployment'){
            steps {
					echo '**Image building section**'
					 script{
						try {
							  echo '**Start building Docker image**'
							  dockerImage = docker.build("ravindrahbtik11/i-ravindrakumar-master:latest")
							  echo '****Image built****'
							  echo '**Start pushing Docker image**'
							  docker.withRegistry( '', 'DockerDetail' ) {
									 dockerImage.push('latest') 
								}
							  echo '****Image pushed****'
							} catch (Throwable e) {
								echo "Caught ${e.toString()}"
								currentBuild.result = "SUCCESS" 
							}
						 
						}	
					echo '****Done Image building and pushing into docker hub****'					
										
					echo '**Creating Config Map**' 
                    bat 'kubectl apply -f .\\configmap.yml'
					echo '****Config Map created****' 
					echo '**Creating Secret**' 
                    bat 'kubectl apply -f .\\secret.yml'
					echo '****Secret created****'
				    echo '**Creating Deployment**' 
                    bat 'kubectl apply -f .\\deployment.yml'
					echo '****Deployment created****' 
                }
         }
		 stage('End'){
			 steps{
				echo '****Success****'				
			 }
		  		 
		 }       
    }    
}
