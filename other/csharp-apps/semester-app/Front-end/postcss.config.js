const autoprefixer = require("autoprefixer");
const tailwindcss = require("tailwindcss");
const cssnano = require("cssnano");
const discardComments = require("postcss-discard-comments");

module.exports = {
  plugins: [
    tailwindcss,
    autoprefixer,
    cssnano({ preset: "default" }),
    discardComments({ removeAll: true }),
  ],
};
