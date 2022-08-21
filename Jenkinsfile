pipeline{
    agent any
    environment{
        scannerHome = tool 'sonar_scanner_dotnet'
        username='admin'
        appname='NAGPDevOpsTest1'   
    }
    options {
        skipDefaultCheckout(true)
    }
    stages{
        stage('Start') {
            steps {
                // Get some code from a GitHub repository sqp_1c72c17192926983467e8beb15cc5bcd1cd19ed4
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
                    bat "dotnet ${scannerHome}\\SonarScanner.MSBuild.dll begin /k:\"NAGPDevops2022Demo\" /d:sonar.login=\"squ_dbe138c55e8feccac167c2a053d72b3fe6231deb\""
                    }
                echo 'Start Sonar qube analysis'
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
                 echo 'test'
                 bat "dotnet test"
            }
        }
        stage('Stop sonarqube analysis') {
                steps {
                echo 'Stopping Sonar Qube analysis'
                  withSonarQubeEnv('Test_Sonar') {
                       bat "dotnet ${scannerHome}\\SonarScanner.MSBuild.dll end /d:sonar.login=\"squ_dbe138c55e8feccac167c2a053d72b3fe6231deb\""
                    }
                echo 'Stopped Sonar Qube analysis'
                }
            }
        
        stage('Release artfact'){
            steps {
                    script{
                         echo 'Start building Docker image'
                          dockerImage = docker.build("ravindrahbtik11/i-ravindrakumar-master:latest")
                          echo 'Image building done'
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
                    bat "gcloud container clusters get-credentials kubernetes-cluster --zone us-central1-c --project nagp48300"
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
       
    }
    post{
        always{
            echo 'I am awsome. I run always'
            //write to logout docker
        }
        success{
            echo 'I run when you are Successful'
        }
        failure{
            echo 'I run when you are fail.'
        }
    //    changed{
    //         echo 'I run when you are fail.'
    //     }
    }
    
}
