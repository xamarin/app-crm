#!/bin/bash

if [ ! -f packages/FAKE/tools/FAKE.exe ]; then
  mono tools/NuGet/NuGet.exe install FAKE -OutputDirectory packages -ExcludeVersion
fi

if [ -z "$2" ]; then
  mono packages/FAKE/tools/FAKE.exe build.fsx $1 # use for iOS
else
	mono packages/FAKE/tools/FAKE.exe build.fsx $1 android_keystore_password=$2 # use for android, passing the keystore password
fi
