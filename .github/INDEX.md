# Agent Framework Documentation Index

## 📚 Quick Navigation

### Getting Started
- **[README_MODERNIZATION.md](README_MODERNIZATION.md)** ← **START HERE**
  - Overview of v2.0 improvements
  - Before/after comparison
  - Quick summary of what's new

### Understanding the System
- **[AGENTS.md](AGENTS.md)** - Workflow & Orchestration
  - Agent workflow diagram
  - Sequential workflow rules
  - Chaining patterns
  - Invocation examples

- **[SPEC-SCHEMA.md](SPEC-SCHEMA.md)** - Complete Specification
  - YAML frontmatter structure
  - Markdown content format
  - Agent-specific extensions
  - Skill-specific extensions
  - Schema validation rules

### Development & Maintenance
- **[MIGRATION.md](MIGRATION.md)** - Upgrade & Maintenance Guide
  - What changed from v1.0 to v2.0
  - Migration path (already applied)
  - Backward compatibility notes
  - Best practices with v2.0

- **[VALIDATION.md](VALIDATION.md)** - Validation & Best Practices
  - Specification validation checklist
  - Composition rules
  - Contract patterns
  - Maintenance guidelines
  - Performance optimization

### Agent Specifications
Individual agents in `.github/agents/`:
- [requirement-agent.md](agents/requirement-agent.md) - Entry point, creates stories
- [plan-agent.md](agents/plan-agent.md) - Generates implementation plans
- [coding-agent.md](agents/coding.agent.md) - Executes code generation
- [testing-agent.md](agents/testing-agent.md) - Creates automation tests
- [commit-agent.md](agents/commit-agent.md) - Finalizes with git commit

### Skills & Patterns
Skills in `.github/skills/`:
- **automation/**: Test automation patterns (Playwright)
- **code-review/**: Code review standards
- **commit/**: Git commit conventions
- **dotnet/**: C# API and testing patterns
- **angular/**: TypeScript/Angular patterns
- **java/**: Java API patterns
- **python/**: Python API patterns
- **shared/**: Reusable patterns (Gherkin, planning, optimization)

## 🎯 By Use Case

### "I want to run agents"
1. Read: [AGENTS.md](AGENTS.md) (workflow section)
2. Follow: Requirement → Plan → Coding/Testing → Commit
3. Refer: Individual agent specs for examples

### "I'm upgrading from v1.0 to v2.0"
1. Read: [README_MODERNIZATION.md](README_MODERNIZATION.md)
2. Review: [MIGRATION.md](MIGRATION.md)
3. Check: [VALIDATION.md](VALIDATION.md) for best practices

### "I want to create a new agent"
1. Study: [SPEC-SCHEMA.md](SPEC-SCHEMA.md) (Agent template)
2. Review: Existing agents as examples
3. Validate: Against [VALIDATION.md](VALIDATION.md) checklist
4. Test: With actual workflow

### "I want to enhance a skill"
1. Review: [SPEC-SCHEMA.md](SPEC-SCHEMA.md) (Skill template)
2. Check: Which agents use this skill
3. Update: YAML metadata + content
4. Validate: Against schema

### "I want to understand agent composition"
1. Read: [AGENTS.md](AGENTS.md) (workflow diagram)
2. Study: YAML `requires`, `chainedWith` fields
3. Review: [SPEC-SCHEMA.md](SPEC-SCHEMA.md) (composition rules)
4. Refer: [VALIDATION.md](VALIDATION.md) (composition patterns)

### "I want to troubleshoot agent errors"
1. Check: Agent spec `rules` section
2. Review: `failures` modes + recovery
3. Verify: Input contracts are satisfied
4. See: Examples in agent spec

## 📋 Document Purposes

| Document | Purpose | Audience | Read Time |
|----------|---------|----------|-----------|
| README_MODERNIZATION.md | Overview & quick reference | Everyone | 10 min |
| AGENTS.md | Workflow orchestration | Operators | 15 min |
| SPEC-SCHEMA.md | Complete specification | Developers | 30 min |
| MIGRATION.md | Upgrade guide | Upgraders | 20 min |
| VALIDATION.md | Best practices & validation | Framework devs | 25 min |
| Individual agent specs | Implementation details | Users & devs | 5-10 min each |

## 🔍 Document Details

### README_MODERNIZATION.md
**What**: High-level overview of v2.0 improvements
**When**: Read first, use as reference
**Covers**:
- What's new (with examples)
- Key improvements table
- New files and updates
- Quick start guide
- Feature highlights
- Future capabilities
- Next steps by role

### AGENTS.md
**What**: Agent workflow and orchestration guide
**When**: Planning and running workflows
**Covers**:
- Workflow diagram
- Agent specifications (role, input, output, dependencies)
- Workflow rules (sequential, parallel, approval gates)
- Agent chaining examples
- Invocation patterns
- Configuration defaults

### SPEC-SCHEMA.md
**What**: Complete specification definition for v2.0
**When**: Creating/updating agents or skills
**Covers**:
- YAML frontmatter metadata (all fields)
- Markdown content sections (all required)
- Agent-specific YAML extensions
- Skill-specific YAML extensions
- Validation rules
- Benefits of structured specs
- Migration path guidance

### MIGRATION.md
**What**: Guide for upgrading and maintaining v2.0 specs
**When**: Updating existing agents or migrating setup
**Covers**:
- What changed (v1.0 vs v2.0)
- Key improvements with examples
- Migration path (already applied for agents)
- Backward compatibility assurance
- Best practices with modernized agents
- Troubleshooting common issues
- Future tooling roadmap

### VALIDATION.md
**What**: Validation rules and best practices guide
**When**: Creating specs or maintaining quality
**Covers**:
- Pre-publishing validation checklist
- Markdown section requirements
- Composition rules and patterns
- Input/output contract patterns
- Rule specification patterns
- Example complete specs
- Maintenance guidelines
- Version update process
- Performance optimization
- Security & compliance
- Monitoring metrics

## 🏗️ Architecture

```
.github/
├── agents/                          # Agent specifications (v2.0)
│   ├── requirement-agent.md
│   ├── plan-agent.md
│   ├── coding.agent.md
│   ├── testing-agent.md
│   └── commit-agent.md
│
├── skills/                          # Reusable skills
│   ├── automation/
│   ├── code-review/
│   ├── commit/
│   ├── dotnet/
│   ├── angular/
│   ├── java/
│   ├── python/
│   └── shared/
│
└── documentation/
    ├── README_MODERNIZATION.md      # Overview (v2.0)
    ├── AGENTS.md                     # Workflow guide
    ├── SPEC-SCHEMA.md                # Complete spec
    ├── MIGRATION.md                  # Upgrade guide
    ├── VALIDATION.md                 # Best practices
    └── INDEX.md                      # This file
```

## 🚀 Workflow

```
┌─────────────────┐
│ User Requirement│
└────────┬────────┘
         │
         ↓
┌─────────────────────────┐
│ @RequirementAgent       │ ← See requirement-agent.md
│ Creates Story + Gherkin │ 
└────────┬────────────────┘
         │ (needs approval)
         ↓
┌─────────────────────────┐
│ @PlanAgent              │ ← See plan-agent.md
│ Creates Plan            │
└────────┬────────────────┘
         │ (needs approval)
         ↓
    ┌────┴────┐
    │ PARALLEL │
    └────┬────┘
    ┌────┴────────────────────────────┐
    │                                 │
    ↓                                 ↓
┌──────────────────┐    ┌────────────────────────┐
│ @CodingAgent     │    │ @TestingAgent          │
│ Code + Tests     │    │ Automation Tests       │
│ + Docs           │    │ + Test Data            │
└────────┬─────────┘    └────────────┬───────────┘
         │                           │
         └────────────┬──────────────┘
                      │
                      ↓
            ┌──────────────────────┐
            │ @CommitAgent         │ ← See commit-agent.md
            │ Reviews + Commits    │
            └──────────────────────┘
```

## 📖 Reading Order by Role

### As a User/Operator
1. [README_MODERNIZATION.md](README_MODERNIZATION.md) (overview)
2. [AGENTS.md](AGENTS.md) (how to run)
3. Individual agent specs (when needed)

### As an Agent Developer
1. [README_MODERNIZATION.md](README_MODERNIZATION.md) (overview)
2. [SPEC-SCHEMA.md](SPEC-SCHEMA.md) (template)
3. [VALIDATION.md](VALIDATION.md) (checklist)
4. Existing agents (as examples)

### As a Framework Developer
1. [README_MODERNIZATION.md](README_MODERNIZATION.md) (overview)
2. [AGENTS.md](AGENTS.md) (orchestration)
3. [SPEC-SCHEMA.md](SPEC-SCHEMA.md) (full spec)
4. [VALIDATION.md](VALIDATION.md) (comprehensive guide)
5. [MIGRATION.md](MIGRATION.md) (implementation guide)

## 🔗 Cross-References

### From AGENTS.md
- Links to individual agent specs
- References to SPEC-SCHEMA for composition
- Refers to workflow rules

### From SPEC-SCHEMA.md
- References README_MODERNIZATION for benefits
- Links to MIGRATION.md for upgrade path
- Points to examples in agent specs

### From VALIDATION.md
- References SPEC-SCHEMA for schema
- Links to individual agent examples
- Points to MIGRATION.md for version updates

### From MIGRATION.md
- References SPEC-SCHEMA for new format
- Links to AGENTS.md for workflows
- Points to individual agents as examples

## 📊 Content Summary

### Key Concepts Covered
- ✓ Agent workflow orchestration
- ✓ Input/output contracts
- ✓ Skill composition and reusability
- ✓ Constraint and rule enforcement
- ✓ Version management
- ✓ Backward compatibility
- ✓ Validation and testing
- ✓ Performance optimization
- ✓ Security compliance

### Tools & Frameworks Referenced
- ✓ .NET (C#)
- ✓ TypeScript/Angular
- ✓ Python
- ✓ Java
- ✓ Playwright (E2E testing)
- ✓ Git/Bitbucket
- ✓ VS Code

### Patterns Documented
- ✓ API development
- ✓ Unit testing
- ✓ UI development
- ✓ Test automation
- ✓ Code review
- ✓ Git commits (conventional)
- ✓ Planning methodology
- ✓ Story writing (Gherkin)

## 💡 Tips

- **Get lost?** Start with [README_MODERNIZATION.md](README_MODERNIZATION.md)
- **Creating agents?** Use [SPEC-SCHEMA.md](SPEC-SCHEMA.md) as template
- **Need examples?** Check individual agent specs in `.github/agents/`
- **Troubleshooting?** See "Failure Modes" in relevant agent spec
- **Need validation?** Use [VALIDATION.md](VALIDATION.md) checklist
- **Upgrading?** Follow [MIGRATION.md](MIGRATION.md) guide

## 📝 Version Info

- **Schema Version**: 2.0
- **Agent Versions**: 2.0.0 (all agents)
- **Documentation**: Current through 2024
- **Backward Compatible**: Yes (v1.0 agents still work)

---

**Last Updated**: 2024
**Schema**: v2.0
**All Agents Updated**: Yes
