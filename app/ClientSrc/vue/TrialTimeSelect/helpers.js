import { parseISO, format } from "date-fns";

// helper functions for the trial date selection components

/**
 * Returns an object of formatted date strings.
 *
 * @param {string} isoDate - YYYY-MM-DD date string
 * @returns {object} - date strings for use in templates
 */
export function formatDate(isoDate) {
  const parsedDate = parseISO(isoDate);
  const dayOfWeek = format(parsedDate, "EEEE");
  const formattedDate = format(parsedDate, "MMMM d, yyyy");

  return {
    isoDate,
    dayOfWeek,
    formattedDate,
  };
}

/**
 * Extracts Vue props from DOM element attributes
 * Converts kebab-case attribute names to camelCase prop names
 * Attempts to parse JSON values for arrays, objects, booleans, and numbers
 *
 * @param {Element} element - The DOM element to extract attributes from
 * @returns {Object} - Object containing the extracted props
 */
export function extractProps(element) {
  const props = {};
  if (!element) {
    return props;
  }

  Array.from(element.attributes).forEach((attr) => {
    let attributeName = attr.name;

    // Remove Vue directive prefixes (:, v-bind:, @, v-on:, etc.)
    if (attributeName.startsWith(":")) {
      attributeName = attributeName.substring(1);
    } else if (attributeName.startsWith("v-bind:")) {
      attributeName = attributeName.substring(7);
    }

    // Convert kebab-case to camelCase for Vue props
    const propName = attributeName.replace(/-([a-z])/g, (g) => g[1].toUpperCase());
    let value = attr.value;

    // Handle empty string attributes
    if (value === "") {
      props[propName] = "";
      return;
    }

    // Try to parse JSON values
    try {
      if (
        value.startsWith("[") ||
        value.startsWith("{") ||
        value === "true" ||
        value === "false" ||
        (!isNaN(value) && value.trim() !== "")
      ) {
        value = JSON.parse(value);
      }
    } catch (e) {
      // Keep as string if not valid JSON
    }

    props[propName] = value;
  });

  return props;
}
