# Frontend Build System

This folder contains the frontend source assets for the SCJ Online Booking application. Build configuration files (`webpack.config.js`, `webpack.config.vendor.js`, `package.json`) are located in the parent directory.

## Build Scripts

```
npm install       # Install dependencies + copy vendor files + cleanup
npm run build     # Build + cleanup
npm run watch     # Build with file watching
```

## Structure

```
frontend/
├── vue/          # Vue.js components (auto-discovered)
│   └── [name]/
│       └── index.js    # Entry point for each component
└── sass/         # SCSS stylesheets
|   ├── _*.scss   # Partials (not compiled directly)
|   └── *.scss    # Compiled to CSS files
└── build-tasks/
    └── script.js # Custom Node.js scripts

```

## Build Process

**Webpack automatically discovers:**

- **Vue components**: Any `frontend/vue/[name]/index.js` becomes `/vue/[name].js`
- **Stylesheets**: Any `frontend/sass/[name].scss` (not starting with `_`) becomes `/css/[name].css`

**Output directory**: All files are bundled to `wwwroot/dist/`

```
wwwroot/dist/
├── vue/
│   ├── ComponentName.js      # JavaScript bundle
│   ├── ComponentName.js.map  # JavaScript source map
│   ├── ComponentName.css     # Extracted CSS styles
│   └── ComponentName.css.map # CSS source map
└── css/
    ├── styles.css           # From frontend/sass/styles.scss
    └── styles.css.map       # Source map (dev builds)
```

## Vendor Dependencies

**webpack.config.vendor.js** copies third-party libraries from `node_modules` folders to `wwwroot/dist/`:

- **jquery** → `lib/jquery.js`, `lib/jquery.min.js`
- **bootstrap** → `lib/bootstrap.js`, `lib/bootstrap.min.js`, `css/bootstrap.css`
- **popper.js** → `lib/popper.js`
- **@fortawesome/fontawesome-free** → `css/fontawesome.css` + webfonts
- **@bcgov/bc-sans** → `css/BCSans.css` + fonts
- **bootstrap-datepicker** → `css/bootstrap-datepicker3.standalone.css`, `lib/bootstrap-datepicker.js`
- **spin.js** → `lib/spin.min.js`, `lib/jquery.spin.js`
- **jquery-validation** → `lib/jquery-validate/`
- **jquery-validation-unobtrusive** → `lib/jquery-validation-unobtrusive/`
- **vue** → bundled into `vendor.js`

These packages are available as standalone files for direct use in Razor views, separate from the Vue component builds.

## Build Tasks

Custom Node.js scripts that supplement webpack functionality, replacing outdated plugins that cause npm warnings.

**post-build.js**:

- Removes unused files from `wwwroot/dist/` root directory.
- Cleans up non-CSS/map files from `wwwroot/dist/css/`.
- Ensures only the necessary compiled assets remain after each build.

## Static Assets

Files outside `wwwroot/dist/` are managed independently of the frontend build process:

- **wwwroot/js/** - Legacy jQuery scripts:
  - `site.js` - Application-wide JavaScript
  - `coa.js` - Court of Appeal specific scripts
  - `sc.js` - Supreme Court specific scripts
- **wwwroot/images/** - Static images and graphics
- **wwwroot/favicon.ico** - Site favicon

These assets are served directly and not affected by the build scripts. The JavaScript structure mirrors the SCSS organization (`global.scss`, `coa.scss`, `sc.scss`) with court-specific and application-wide files.
