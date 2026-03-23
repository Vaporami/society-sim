# Building

## Linux

Clone the repo with this command (basic git clone with the flag to clone the submodules too):
```bash
git clone https://github.com/Vaporami/society-sim --recurse-submodules
```

Then, you should run build-linux.sh file on linux. It's gonna prepare a sim-settings directory with default settings and then build the whole thing.

## Windows

Clone the repo with this command (basic git clone with the flag to clone the submodules too):
```bash
git clone https://github.com/Vaporami/society-sim --recurse-submodules
```

Then, create a directory called "sim-settings" and copy json-files from the default\jsons\ directory into it. After that, build this with... Whatever. :P I believe you can use pretty much anything that builds .NET from a .csproj file. Pretty sure that VS or Rider are capable of that. No additional settings required... I believe.