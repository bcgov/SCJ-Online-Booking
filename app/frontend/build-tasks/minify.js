const fs = require("fs");
const path = require("path");
const { minify } = require("terser");

/**
 * Minifies a JavaScript file and saves it to the specified output directory.
 *
 * @async
 * @function minifyJs
 * @param {string} relativePath - The relative path to the JavaScript file (without .js extension) from the wwwroot directory
 * @param {string} [outputDirOverride] - Optional override for the output directory. If not provided, defaults to "dist/minified"
 * @param {boolean} [generateMap=false] - Optional flag to generate source maps. If true, creates a .min.js.map file
 * @returns {Promise<void>} A promise that resolves when the minification is complete
 * @throws {Error} Throws an error if the input file is not found or if the minification process fails
 *
 * @description
 * This function reads a JavaScript file from the wwwroot directory, minifies it using Terser,
 * and writes the minified output to a .min.js file in the specified output directory.
 * If the input file doesn't exist, the function throws an error.
 * The output directory is created recursively if it doesn't exist.
 *
 * @example
 * // Minify a file to the default location (wwwroot/dist/minified/)
 * await minifyJs('js/site');
 *
 * @example
 * // Minify a file to a custom output directory within wwwroot
 * await minifyJs('dist/lib/jquery.spin', 'dist/lib');
 *
 * @example
 * // Minify a file and generate source map
 * await minifyJs('js/site', null, true);
 */
async function minifyJs(relativePath, outputDirOverride, generateMap = false) {
  const wwwrootDir = path.join(__dirname, "..", "..", "wwwroot");
  const inputPath = path.join(wwwrootDir, relativePath + ".js");

  if (!fs.existsSync(inputPath)) {
    throw new Error(`Input file not found: ${inputPath}`);
  }

  const filename = path.basename(relativePath);

  const outputDir = outputDirOverride
    ? path.join(wwwrootDir, outputDirOverride)
    : path.join(wwwrootDir, "dist", "minified");

  const outputPath = path.join(outputDir, filename + ".min.js");

  fs.mkdirSync(outputDir, { recursive: true });

  const code = fs.readFileSync(inputPath, "utf8");

  const terserOptions = generateMap
    ? { sourceMap: { filename: filename + ".min.js", url: filename + ".min.js.map" } }
    : {};

  const result = await minify(code, terserOptions);

  if (result.error) throw result.error;

  fs.writeFileSync(outputPath, result.code);

  if (generateMap && result.map) {
    const mapPath = path.join(outputDir, filename + ".min.js.map");
    fs.writeFileSync(mapPath, result.map);
  }
}

module.exports = { minifyJs };
