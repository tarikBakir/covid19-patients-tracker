@Library('shared-library') _




    // Defining a dictionary with paths as keys and parameters as values to run dotnet build command

    def dotnetBuildParams = [

		"\\Project1\\dotnetProject1.csproj": "/p:configuration=Release" 

		"\\Project2\\dotnetProject2.csproj": "/p:configuration=Release"

		]

	

    // Defining a dictionary with paths as keys and parameters as values to run dotnet publish command

    def dotnetPublishParams = [

		"\\Project3\\dotnetProject3.csproj": "/p:configuration=Release /p:PublishProfile=FolderProfile -o ..\\..\\Publish\\Project3\\dotnetProject3"

		"\\Project4\\dotnetProject4.csproj": "/p:configuration=Release /p:PublishProfile=FolderProfile -o ..\\..\\Publish\\Project4\\dotnetProject4"

		]

	

	// Defining a dictionary with paths as keys and parameters as values to run dotnet test command 	

	def dotnetTestParams = [

	    "\\Tests\\Project1\\dotnetProject1.csproj": "--configuration Release"

		"\\Tests\\Project2\\dotnetProject2.csproj": "--configuration Release"

		"\\Tests\\Project3\\dotnetProject3.csproj": "--configuration Release"

		"\\Tests\\Project4\\dotnetProject4.csproj": "--configuration Release"

	    ]


pipeline {
    agent any

    stages {
            stage('Build') {
				steps {
                    dotnet("publish",dotnetBuildParams)	
			    }
		    }
		
			stage ('Publish'){
				steps {
					dotnet("test",dotnetPublishParams)
				}
			}
			
			stage('Test') {
				steps {
					dotnet("test",dotnetTestParams)
			}
		}
    }
}
