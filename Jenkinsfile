pipeline {
    agent any

    stages {
        stage ('Clean workspace') {
            steps {
                cleanWs()
            }
            
        stage('Restore packages') {
            steps {
                bat "dotnet restore ${workspace}\\covid19-patients-tracker\\covid19-patients-tracker.sln"
            }
        }
            
        stage('Clean') {
            steps {
                bat "msbuild.exe ${workspace}\\covid19-patients-tracker\\covid19-patients-tracker.sln" /nologo /nr:false /p:platform=\"x64\" /p:configuration=\"release\" /t:clean"
            }
        }
    }
}
