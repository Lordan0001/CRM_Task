import { Button } from "@/components/ui/button";

export default function ContactTable({ contacts, onEdit, onDelete }) {
  return (
    <div className="overflow-hidden rounded-[1.75rem] border border-slate-800 bg-slate-950/90 shadow-xl shadow-slate-950/20">
      <div className="flex flex-col gap-4 border-b border-slate-800 px-6 py-5 sm:flex-row sm:items-center sm:justify-between">
        <div>
          <p className="text-sm uppercase tracking-[0.24em] text-slate-500">
            Contacts list
          </p>
          <h2 className="mt-2 text-2xl font-semibold text-white">All people</h2>
        </div>
        <p className="text-sm text-slate-400">
          {contacts.length} contacts available
        </p>
      </div>

      <div className="overflow-x-auto">
        <table className="min-w-full divide-y divide-slate-800 text-left text-sm text-slate-200">
          <thead className="bg-slate-950">
            <tr>
              <th className="px-6 py-4 font-semibold text-slate-400">Name</th>
              <th className="px-6 py-4 font-semibold text-slate-400">Phone</th>
              <th className="px-6 py-4 font-semibold text-slate-400">Job</th>
              <th className="px-6 py-4 font-semibold text-slate-400">
                Birth date
              </th>
              <th className="px-6 py-4 font-semibold text-slate-400">
                Actions
              </th>
            </tr>
          </thead>
          <tbody className="divide-y divide-slate-800 bg-slate-900/80">
            {contacts.length === 0 ? (
              <tr>
                <td
                  className="px-6 py-8 text-center text-slate-500"
                  colSpan="5"
                >
                  No contacts yet — add one to get started.
                </td>
              </tr>
            ) : (
              contacts.map((contact) => (
                <tr
                  key={contact.id}
                  className="transition-colors duration-200 hover:bg-slate-950/80"
                >
                  <td className="whitespace-nowrap px-6 py-4 font-medium text-white">
                    {contact.name}
                  </td>
                  <td className="px-6 py-4 text-slate-300">
                    {contact.mobilePhone}
                  </td>
                  <td className="px-6 py-4 text-slate-300">
                    {contact.jobTitle}
                  </td>
                  <td className="px-6 py-4 text-slate-300">
                    {contact.birthDate?.substring(0, 10)}
                  </td>
                  <td className="px-6 py-4 space-x-2">
                    <Button
                      variant="outline"
                      size="sm"
                      onClick={() => onEdit(contact)}
                    >
                      Edit
                    </Button>
                    <Button
                      variant="destructive"
                      size="sm"
                      onClick={() => onDelete(contact.id)}
                    >
                      Delete
                    </Button>
                  </td>
                </tr>
              ))
            )}
          </tbody>
        </table>
      </div>
    </div>
  );
}
