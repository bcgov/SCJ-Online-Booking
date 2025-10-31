const fs = require("fs");
const path = require("path");
const { minify } = require("terser");

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

// Minify jquery.spin.js from wwwroot/dist/lib
(async () => {
  const inputPath = path.join(distDir, "lib", "jquery.spin.js");
  const outputPath = path.join(distDir, "lib", "jquery.spin.min.js");

  if (fs.existsSync(inputPath)) {
    const code = fs.readFileSync(inputPath, "utf8");
    const result = await minify(code);
    if (result.error) throw result.error;
    fs.writeFileSync(outputPath, result.code);
  }
})();
