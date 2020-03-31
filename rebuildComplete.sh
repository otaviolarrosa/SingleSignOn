#!/bin/sh
dotnet clean SingleSignOn.sln && \
find -type d -name "bin" | while read -r bin_folder ; do rm -rf "$bin_folder"; done && \
find -type d -name "obj" | while read -r obj_folder ; do rm -rf "$obj_folder"; done && \
dotnet build SingleSignOn.sln
