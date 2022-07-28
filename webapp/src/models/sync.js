import { synchronize } from "@nozbe/watermelondb/sync";
import { database } from "models/database";

export async function syncAppWithRemote() {
  await synchronize({
    database,
    pullChanges: async ({ lastPulledAt, schemaVersion, migration }) => {
      console.log({ lastPulledAt, schemaVersion, migration });
      const urlParams = `lastSyncAt=${lastPulledAt ?? 0}&schema_version=${schemaVersion ?? 0}&migration=${encodeURIComponent(
        JSON.stringify(migration),
      )}`;
      const response = await fetch(`https://localhost:44357/sync?${urlParams}`);
      if (!response.ok) {
        throw new Error(await response.text());
      }

      const { changes, timestamp } = await response.json();
      console.log("Pull response",{changes, timestamp})
      return { changes, timestamp };
    },
    pushChanges: async ({ changes, lastPulledAt }) => {
        console.log({changes, lastPulledAt})
      const response = await fetch(`https://localhost:44357/sync`, {
        method: "POST",
        headers:{"content-type": "application/json"},
        body: JSON.stringify({changes}),
      });
      if (!response.ok) {
        throw new Error(await response.text());
      }
    },
    migrationsEnabledAtVersion: 1,
  });
}


