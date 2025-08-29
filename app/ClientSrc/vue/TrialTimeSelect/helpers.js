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
