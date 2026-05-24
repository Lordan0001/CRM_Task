export function validateContact(c) {
  const errors = {};

  if (!c.name || c.name.length < 2) {
    errors.name = "Name must be at least 2 characters";
  }

  if (!c.mobilePhone || !/^\+?[0-9]{10,15}$/.test(c.mobilePhone)) {
    errors.mobilePhone = "Invalid phone format";
  }

  if (!c.jobTitle) {
    errors.jobTitle = "Job title is required";
  }

  if (!c.birthDate || new Date(c.birthDate) >= new Date()) {
    errors.birthDate = "Birth date must be in the past";
  }

  return errors;
}
