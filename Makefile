.PHONY: all clean build start

all: clean restore build

clean:
	dotnet clean

restore:
	dotnet restore

build:
	dotnet build

test:
	dotnet test
	
format:
	dotnet format
