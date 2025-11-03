const fs = require("fs");
const path = require("path");
const { minifyJs } = require("./minify");

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

(async () => {
  // Minify custom application scripts to /dist/minified.
  // Generate source maps for easier debugging.
  await minifyJs("js/coa", null, true);
  await minifyJs("js/sc", null, true);
  await minifyJs("js/site", null, true);

  // Minify unminified vendor libraries.
  // These don't need source maps because they are 3rd party libraries.
  await minifyJs("dist/lib/jquery.spin", "dist/lib");
})();
