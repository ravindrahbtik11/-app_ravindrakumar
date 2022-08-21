pipeline{
    agent any
     options {
        skipDefaultCheckout(true)
     }
    stages{
        stage('Start') {
            steps {
                // Get some code from a GitHub repository sqp_1c72c17192926983467e8beb15cc5bcd1cd19ed4
                echo 'Starting code check out'
                git branch: 'develop', url: 'https://github.com/ravindrahbtik11/app_ravindrakumar.git'
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
       stage('Code build'){
            steps {
                echo 'Build solution'
                bat "dotnet build"
                echo 'Build success'
            }
        }        
        stage('Release artfact'){
            steps {
                    script{
                         echo 'Start building Docker image'
                          dockerImage = docker.build("ravindrahbtik11/i-ravindrakumar-develop:latest")
                          echo 'Image building done'
                          echo 'Start pushing Docker image'
                          docker.withRegistry( '', 'DockerDetail' ) {
                                 dockerImage.push() 
                            }
                            echo 'Image pushing done'
                        }
                }
         }
		stage('Kubernetes deployment'){
            steps {
                    echo 'Connecting to cluster'
                    bat 'gcloud container clusters get-credentials kubernetes-cluster --zone us-central1-c --project nagp48300'
					echo 'Connected' 
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
            steps {
                    echo 'Success'
                }
         }
       
    }
     
}