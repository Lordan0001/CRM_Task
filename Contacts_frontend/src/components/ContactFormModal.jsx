import { useEffect, useState } from "react";
import { Button } from "@/components/ui/button";
import { validateContact } from "../validation/validateContact";

export default function ContactFormModal({ open, onClose, onSave, initial }) {
  const [form, setForm] = useState({
    name: "",
    mobilePhone: "",
    jobTitle: "",
    birthDate: "",
  });

  const [errors, setErrors] = useState({});
  const [apiError, setApiError] = useState("");

  useEffect(() => {
    if (open && initial) {
      setForm(initial);
    }
    if (!open) {
      setForm({ name: "", mobilePhone: "", jobTitle: "", birthDate: "" });
      setErrors({});
      setApiError("");
    }
  }, [initial, open]);

  if (!open) return null;

  function handleChange(e) {
    const { name, value } = e.target;
    setForm((prev) => ({ ...prev, [name]: value }));
  }

  async function handleSubmit() {
    const validation = validateContact(form);

    if (Object.keys(validation).length > 0) {
      setErrors(validation);
      return;
    }

    setErrors({});
    setApiError("");

    try {
      await onSave(form);
      onClose();
    } catch (error) {
      const detail =
        error?.response?.data?.detail ||
        error?.response?.data?.title ||
        error?.message ||
        "Server error. Please try again.";
      setApiError(detail);
    }
  }

  return (
    <div className="fixed inset-0 z-50 flex items-center justify-center bg-slate-950/80 p-4 backdrop-blur-sm">
      <div className="w-full max-w-2xl overflow-hidden rounded-[2rem] border border-slate-800 bg-slate-900/95 p-8 shadow-2xl shadow-slate-950/40">
        <div className="flex flex-col gap-4 sm:flex-row sm:items-start sm:justify-between">
          <div>
            <p className="text-sm uppercase tracking-[0.24em] text-sky-400/80">
              Contact form
            </p>
            <h3 className="mt-2 text-2xl font-semibold text-white">
              {initial ? "Edit contact" : "Create new contact"}
            </h3>
          </div>
          <Button variant="ghost" onClick={onClose}>
            Close
          </Button>
        </div>

        <div className="mt-6 grid gap-4 sm:grid-cols-2">
          <label className="space-y-2 text-sm text-slate-100">
            <span className="font-medium text-slate-300">Name</span>
            <input
              className="w-full rounded-3xl border border-slate-800 bg-slate-950/90 px-4 py-3 text-sm text-white outline-none transition focus:border-sky-400/70 focus:ring-2 focus:ring-sky-500/20"
              name="name"
              placeholder="Full name"
              onChange={handleChange}
              value={form.name}
            />
            {errors.name && (
              <p className="text-xs text-rose-400">{errors.name}</p>
            )}
          </label>

          <label className="space-y-2 text-sm text-slate-100">
            <span className="font-medium text-slate-300">Phone</span>
            <input
              className="w-full rounded-3xl border border-slate-800 bg-slate-950/90 px-4 py-3 text-sm text-white outline-none transition focus:border-sky-400/70 focus:ring-2 focus:ring-sky-500/20"
              name="mobilePhone"
              placeholder="+123 456 7890"
              onChange={handleChange}
              value={form.mobilePhone}
            />
            {errors.mobilePhone && (
              <p className="text-xs text-rose-400">{errors.mobilePhone}</p>
            )}
          </label>

          <label className="space-y-2 text-sm text-slate-100">
            <span className="font-medium text-slate-300">Job title</span>
            <input
              className="w-full rounded-3xl border border-slate-800 bg-slate-950/90 px-4 py-3 text-sm text-white outline-none transition focus:border-sky-400/70 focus:ring-2 focus:ring-sky-500/20"
              name="jobTitle"
              placeholder="Product manager"
              onChange={handleChange}
              value={form.jobTitle}
            />
            {errors.jobTitle && (
              <p className="text-xs text-rose-400">{errors.jobTitle}</p>
            )}
          </label>

          <label className="space-y-2 text-sm text-slate-100">
            <span className="font-medium text-slate-300">Birth date</span>
            <input
              className="w-full rounded-3xl border border-slate-800 bg-slate-950/90 px-4 py-3 text-sm text-white outline-none transition focus:border-sky-400/70 focus:ring-2 focus:ring-sky-500/20"
              type="date"
              name="birthDate"
              onChange={handleChange}
              value={form.birthDate}
            />
            {errors.birthDate && (
              <p className="text-xs text-rose-400">{errors.birthDate}</p>
            )}
          </label>
        </div>

        {apiError ? (
          <div className="rounded-3xl border border-rose-500/20 bg-rose-500/10 px-4 py-3 text-sm text-rose-200">
            {apiError}
          </div>
        ) : null}

        <div className="mt-8 flex flex-col gap-3 sm:flex-row sm:justify-end">
          <Button
            variant="outline"
            onClick={onClose}
            className="w-full sm:w-auto"
          >
            Cancel
          </Button>
          <Button className="w-full sm:w-auto" onClick={handleSubmit}>
            Save contact
          </Button>
        </div>
      </div>
    </div>
  );
}
