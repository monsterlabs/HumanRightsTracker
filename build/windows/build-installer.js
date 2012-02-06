// This script was started by copying MonoDevelop's, available at 
// https://github.com/mono/monodevelop/tree/master/setup/WixSetup

// HEAT manual: http://wix.sourceforge.net/manual-wix3/heat.htm

var bin = '..\\..\\bin';
var sh = new ActiveXObject("WScript.Shell");
var fs = new ActiveXObject("Scripting.FileSystemObject");
var env = sh.Environment("Process");
var heat = "\"" + env("WIX") + "bin\\heat.exe\"";

// Look for msbuild.exe
if (fs.FileExists (env("windir") + "\\Microsoft.NET\\Framework\\v4.0.30319\\msbuild.exe") == 1) {
  var msbuild = env("windir") + "\\Microsoft.NET\\Framework\\v4.0.30319\\msbuild.exe"
} else if (fs.FileExists(env("windir") + "\\Microsoft.NET\\Framework\\v3.5\\msbuild.exe") == 1) {
  var msbuild = env("windir") + "\\Microsoft.NET\\Framework\\v3.5\\msbuild.exe"
} else {
  WScript.Echo ('Build failed: Microsoft.NET MSBuild \(msbuild.exe\) not found');
  WScript.Quit (1);
}

// Could build from here, but atm at least I prefer to assume it's already built
//build ("..\\..\\HumanRightsTracker.sln");

// Delete some files that might be created by running uninstalled
if (fs.FileExists (bin + "\\bin\\registry.bin")) fs.DeleteFile (bin + "\\bin\\registry.bin");
if (fs.FolderExists (bin + "\\bin\\addin-db-001")) fs.DeleteFolder (bin + "\\bin\\addin-db-001");

// We can't just heat the entire dir b/c it would include the .git/ directory
heatDir ("bin");
heatDir ("etc");
heatDir ("lib");
heatDir ("share\\icons");
heatDir ("share\\themes");

// Create the installer, will be outputted to Banshee-1.9.6.msi in build/windows/
build ("Installer.wixproj")
WScript.Echo ("Setup successfully generated");

function heatDir (dir)
{
  var wxi_name = dir.replace (/\\/, '_');
  var params = ' -cg ' + dir + ' -scom -sreg -ag -sfrag -indent 2 -var var.' + wxi_name + 'Dir';
 
  if (wxi_name.indexOf ("share") == 0) {
	  params += ' -dr SHARELOCATION ';
  } else {
	  params += ' -dr INSTALLLOCATION ';
  }

  if (dir == 'bin') {
    // Do not auto-generate ids for files in the bin directory
    params += '-suid '
  }
  // Generate the list of binary files (managed and native .dlls and .pdb and .config files)
  run (heat + ' dir ..\\..\\bin\\' + dir + params + ' -out obj\\generated_'+wxi_name+'.wxi');

  // Heat has no option to output Include (wxi) files instead of Wix (wxs) ones, so do a little regex
  regexreplace ('obj\\generated_'+wxi_name+'.wxi', /Wix xmlns/, 'Include xmlns');
  regexreplace ('obj\\generated_'+wxi_name+'.wxi', /Wix>/, 'Include>');
}

function run (cmd)
{
  if (sh.run (cmd, 5, true) != 0) {
    WScript.Echo ("Failed to run cmd:\n" + cmd);
    WScript.Quit (1);
  }
}

function build (file)
{
  if (sh.run (msbuild + " " + file, 5, true) != 0) {
    WScript.Echo ("Build failed");
    WScript.Quit (1);
  }
}

function regexreplace (file, regex, replacement)
{
  var f = fs.OpenTextFile (file, 1);
  var content = f.ReadAll ();
  f.Close ();
  content = content.replace (regex, replacement);
  f = fs.CreateTextFile (file, true);
  f.Write (content);
  f.Close ();
}
