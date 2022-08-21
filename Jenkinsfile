pipeline{
    agent any
    environment{
        scannerHome = tool 'sonar_scanner_dotnet'
        username='admin'
        appname='sonar-ravindrakumar'   
    }
    options {
        skipDefaultCheckout(true)
    }
    stages{
        stage('Start') {
            steps {
                echo 'Starting code check out'
                git branch: 'master', url: 'https://github.com/ravindrahbtik11/app_ravindrakumar.git'
                 echo 'Code check out Finished'
            }
        }
    stage('Nuget restore'){
            steps{
                    echo 'Start restoring packages'
                    bat "dotnet restore"
                    echo 'Restore Nuget success'
                }
        }
    stage('Start sonarqube analysis') {
            steps {
                echo 'Start Sonar qube analysis'
                    withSonarQubeEnv('Test_Sonar') {
                    bat "dotnet ${scannerHome}\\SonarScanner.MSBuild.dll begin /k:\"sonar-ravindrakumar\" /d:sonar.login=\"sqp_c18f63960a2505f4912fbde8ae301342d4ef4a84\""
                    }
                echo 'Finished Sonar qube analysis'
            }
        }

    stage('Code build'){
            steps {
                echo 'Build solution'
                bat "dotnet build"
                echo 'Build success'
            }
        }

        stage('Test case execution'){
            steps {                
                 bat "dotnet test"
            }
        }
        stage('Stop sonarqube analysis') {
                steps {
                echo 'Stopping Sonar Qube analysis'
                  withSonarQubeEnv('Test_Sonar') {
                       bat "dotnet ${scannerHome}\\SonarScanner.MSBuild.dll end /d:sonar.login=\"sqp_c18f63960a2505f4912fbde8ae301342d4ef4a84\""
                    }
                echo 'Stopped Sonar Qube analysis'
                }
            }
        
        stage('Release artfact'){
            steps {
                    script{
                         echo 'Start building Docker image'
                          dockerImage = docker.build("ravindrahbtik11/i-ravindrakumar-master:latest")
                          echo 'Image built'
                          echo 'Start pushing Docker image'
                          docker.withRegistry( '', 'DockerDetail' ) {
                                 dockerImage.push() 
                            }
                            echo 'Image pushed'
                        }
                }
         }
		 stage('Kubernetes deployment'){
            steps {
                    echo 'Connecting to cluster'
                    bat 'gcloud container clusters get-credentials kubernetes-cluster --zone us-central1-c --project nagp48300'
					echo 'Connected' 
					echo 'Deleting existing deployment' 
					bat 'kubectl delete -f .\\deployment.yml'
					echo 'Creating Config Map' 
                    bat 'kubectl apply -f .\\configmap.yml'
					echo 'Config Map created' 
					echo 'Creating Secret' 
                    bat 'kubectl apply -f .\\secret.yml'
					echo 'Secret created'
				    echo 'Creating Deployment' 
                    bat 'kubectl apply -f .\\deployment.yml'
					echo 'Deployment created' 
                }
         }
		 stage('End'){
			 steps{
				echo 'Success'				
			 }
		  		 
		 }       
    }    
}
