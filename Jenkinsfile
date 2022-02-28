pipeline {
    agent any

    stages {
        stage('Restore packages'){
           steps{
               bat "dotnet restore covid19-patients-tracker\\covid19-patients-tracker.csproj"
            }
         }
    }
}
