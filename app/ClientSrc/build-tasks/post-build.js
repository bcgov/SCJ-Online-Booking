const fs = require("fs");
const path = require("path");

// Delete all files from the root of wwwroot/dist after a build to get rid of unused files
const distDir = path.join(__dirname, "..", "..", "..", "app", "wwwroot", "dist");
fs.readdirSync(distDir).forEach((item) => {
  const itemPath = path.join(distDir, item);
  if (fs.statSync(itemPath).isFile()) {
    fs.unlinkSync(itemPath);
  }
});

// Delete files from wwwroot/dist/css that don't end in .map or .css
const cssDir = path.join(distDir, "css");
if (fs.existsSync(cssDir)) {
  fs.readdirSync(cssDir).forEach((item) => {
    const itemPath = path.join(cssDir, item);
    if (fs.statSync(itemPath).isFile() && !item.endsWith(".css") && !item.endsWith(".map")) {
      fs.unlinkSync(itemPath);
    }
  });
}
