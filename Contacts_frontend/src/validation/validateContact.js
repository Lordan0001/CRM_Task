export function validateContact(contact) {
  const errors = {};

  if (!contact.name || contact.name.length < 2) {
    errors.name = "Name must be at least 2 characters";
  }

  if (!contact.mobilePhone || !/^\+?[0-9]{10,15}$/.test(contact.mobilePhone)) {
    errors.mobilePhone = "Invalid phone format";
  }

  if (!contact.jobTitle) {
    errors.jobTitle = "Job title is required";
  }

  if (!contact.birthDate || new Date(contact.birthDate) >= new Date()) {
    errors.birthDate = "Birth date must be in the past";
  }

  return errors;
}
