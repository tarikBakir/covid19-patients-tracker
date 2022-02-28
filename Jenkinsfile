pipeline {
    agent any

    stages {
            stage('Build') {
		steps {
                    bat '"C:\\tools\\nuget.exe" restore covid19-patients-tracker.sln'
		}
	    }
    }
}
