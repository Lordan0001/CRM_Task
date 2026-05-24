import axios from "axios";

const BASE_URL = "https://localhost:5001/api/contact/contacts";

const api = axios.create({
  baseURL: BASE_URL,
  headers: {
    "Content-Type": "application/json",
  },
});

export async function getContacts() {
  const response = await api.get("");
  return response.data;
}

export async function createContact(data) {
  const response = await api.post("", data);
  return response.data;
}

export async function updateContact(id, data) {
  const response = await api.put(`/${id}`, data);
  return response.data;
}

export async function deleteContact(id) {
  const response = await api.delete(`/${id}`);
  return response.data;
}
