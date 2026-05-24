import { useEffect, useState } from "react";
import {
  getContacts,
  createContact,
  updateContact,
  deleteContact,
} from "../api/contactsApi";

export function useContacts() {
  const [contacts, setContacts] = useState([]);

  async function load() {
    const data = await getContacts();
    setContacts(data);
  }

  useEffect(() => {
    load();
  }, []);

  async function add(contact) {
    await createContact(contact);
    await load();
  }

  async function update(id, contact) {
    await updateContact(id, contact);
    await load();
  }

  async function remove(id) {
    await deleteContact(id);
    await load();
  }

  return { contacts, add, update, remove };
}
