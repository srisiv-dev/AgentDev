# Conventional Commits

## Commit Types

| Type | Purpose | Example |
|------|---------|----------|
| `feat:` | New feature | `feat: add user authentication` |
| `fix:` | Bug fix | `fix: resolve login timeout issue` |
| `refactor:` | Code restructuring (no behavior change) | `refactor: simplify validation logic` |
| `test:` | Test additions/updates | `test: add edge case coverage for payment` |
| `docs:` | Documentation updates | `docs: update API endpoint reference` |
| `perf:` | Performance improvements | `perf: optimize database query` |
| `chore:` | Maintenance (dependencies, CI) | `chore: update dependencies` |

## Format

```
<type>: <short description (max 50 chars)>

<optional detailed explanation>

Closes: #<issue-number>
```

## Rules

✓ Use imperative mood ("add" not "adds" or "added")
✓ Don't capitalize first letter after colon
✓ No period at end of subject line
✓ Include issue/story reference when applicable
