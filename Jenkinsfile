pipeline {
    agent any

    stages {
        stage('Restore packages'){
           steps{
               sh 'dotnet restore covid19-patients-tracker.sln'
            }
         }
        stage('Clean'){
           steps{
               sh 'dotnet clean covid19-patients-tracker.sln --configuration Release'
            }
         }
        stage('Build'){
           steps{
               sh 'dotnet build covid19-patients-tracker.sln --configuration Release --no-restore'
            }
         }
        stage('Test: Unit Test'){
           steps {
                sh 'dotnet test XUnitTestProject/XUnitTestProject.csproj --configuration Release --no-restore'
             }
          }
    }
}
