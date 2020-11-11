SHELL	             = /bin/bash
SRC                  = src/

# Settings
CONFIG              ?= Release
NUGET_DIR           ?= nuget
TEST_PROPERTIES     ?= /p:CollectCoverage=true

# DLL metadata
GIT_BRANCH           = $(shell git rev-parse --abbrev-ref HEAD)
GIT_HASH             = $(shell git rev-parse HEAD)
GIT_DATE             = $(shell git log -1 --format='%at')
VERSION              = $(shell date --utc -d @"$(GIT_DATE)" +'%-y.%-m.%-d.%-H%M')
PACKAGE_PROPERTIES   = /p:Version="$(VERSION)" /p:RepositoryBranch="$(GIT_BRANCH)" /p:RepositoryCommit="$(GIT_HASH)"

.PHONY: build test clean

all: clean test $(NUGET_DIR)

$(NUGET_DIR): build
	mkdir -p $@
    		
	dotnet pack --configuration $(CONFIG) $(PACKAGE_PROPERTIES) --no-build --output $@ $(SRC)

build:
	dotnet build --configuration $(CONFIG) $(PACKAGE_PROPERTIES) $(SRC)
	
test: build
	dotnet test --configuration $(CONFIG) $(TEST_PROPERTIES) --no-build $(SRC)
	
clean:
	rm -rf $(NUGET_DIR)
