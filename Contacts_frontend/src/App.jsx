import { useState } from "react";
import { Button } from "@/components/ui/button";
import { useContacts } from "./hooks/useContacts";
import ContactTable from "./components/ContactTable";
import ContactFormModal from "./components/ContactFormModal";

export default function App() {
  const { contacts, add, update, remove } = useContacts();
  const [open, setOpen] = useState(false);
  const [selected, setSelected] = useState(null);

  const total = contacts.length;

  function handleEdit(contact) {
    setSelected(contact);
    setOpen(true);
  }

  function handleAdd() {
    setSelected(null);
    setOpen(true);
  }

  async function handleSave(data) {
    if (selected) {
      await update(selected.id, data);
    } else {
      await add(data);
    }
  }

  return (
    <div className="min-h-screen bg-slate-950 text-slate-100">
      <div className="mx-auto max-w-6xl px-4 py-10 sm:px-6 lg:px-8">
        <div className="overflow-hidden rounded-[2rem] border border-slate-800 bg-slate-900/90 p-8 shadow-2xl shadow-slate-950/40 backdrop-blur-xl">
          <div className="flex flex-col gap-6 lg:flex-row lg:items-center lg:justify-between">
            <div>
              <p className="text-sm uppercase tracking-[0.24em] text-sky-400/80">
                Contacts dashboard
              </p>
              <h1 className="mt-3 text-4xl font-semibold tracking-tight text-white sm:text-5xl">
                Beautiful contact management
              </h1>
              <p className="mt-4 max-w-2xl text-slate-400 sm:text-lg">
                Contacts list
              </p>
            </div>
            <div className="flex items-center gap-3">
              <Button onClick={handleAdd}>Add contact</Button>
              <Button
                variant="outline"
                size="sm"
                onClick={() => setOpen(false)}
              >
                Close panel
              </Button>
            </div>
          </div>

          <div className="mt-8 grid gap-4 sm:grid-cols-3">
            <div className="rounded-[1.5rem] border border-slate-800 bg-slate-950/80 p-5">
              <p className="text-sm text-slate-500">Total contacts</p>
              <p className="mt-3 text-3xl font-semibold text-white">{total}</p>
            </div>
            <div className="rounded-[1.5rem] border border-slate-800 bg-slate-950/80 p-5">
              <p className="text-sm text-slate-500">Quick actions</p>
              <p className="mt-3 text-slate-300">
                Edit, delete and add contacts in a clean responsive layout.
              </p>
            </div>
            <div className="rounded-[1.5rem] border border-slate-800 bg-slate-950/80 p-5">
              <p className="text-sm text-slate-500">Built with</p>
              <p className="mt-3 text-slate-300">
                Tailwind CSS + shadcn button component
              </p>
            </div>
          </div>

          <div className="mt-10">
            <ContactTable
              contacts={contacts}
              onEdit={handleEdit}
              onDelete={remove}
            />
          </div>
        </div>
      </div>

      <ContactFormModal
        open={open}
        initial={selected}
        onClose={() => setOpen(false)}
        onSave={handleSave}
      />
    </div>
  );
}
