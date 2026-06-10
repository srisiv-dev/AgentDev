# Migration Guide: Agent Spec Modernization

## Overview

Your agents have been modernized from v1.0 to v2.0 while preserving the spec-driven model. The new version includes:

- ✅ Structured YAML frontmatter with rich metadata
- ✅ Input/output contracts for validation
- ✅ Enhanced tool specifications
- ✅ Explicit validation rules and constraints
- ✅ Agent composition and chaining support
- ✅ Improved documentation with examples

## What Changed

### YAML Frontmatter (v2.0)

**Before (v1.0)**:
```yaml
---
name: Coding Agent
description: Execute approved implementation plan...
model: claude-3-5-sonnet
tools:
  - editFiles
  - readFiles
---
```

**After (v2.0)**:
```yaml
---
apiVersion: "2.0"
kind: Agent
name: coding-agent
title: Coding Agent
description: Execute approved implementation plan...
version: "2.0.0"
agentType: execution
position: 3
model: claude-3-5-sonnet
temperature: 0.2
maxTokens: 16000
timeout: 1800
requires: [dotnet/api, dotnet/unit-test, ...]
inputContract:
  required: [approvedPlan]
  format: "plan.md from Plan Agent"
outputContract:
  primary: code
  secondary: [unitTests, documentation]
tools:
  - name: editFiles
    capability: write
    requiredFor: [generatingProductionCode]
constraints:
  maxExecutionTime: 1800
  approvalRequired: false
rules:
  - condition: "plan is ambiguous"
    action: "STOP and request clarification"
    severity: "error"
---
```

### Markdown Content

**Enhanced sections**:
- Clear ROLE statement
- Responsibilities (what agent does/doesn't do)
- Input Requirements (what's needed to execute)
- Processing Rules (step-by-step approach)
- Output Format (deliverable structure)
- Validation Checklist (before completion)
- Failure Modes (with recovery strategies)
- Dependencies (skills, tools, standards)
- Examples (concrete execution samples)

## Key Improvements

### 1. Composition & Chaining
- `requires`: Skills/agents this agent depends on
- `chainedWith`: Agents that follow in workflow
- `canChainTo`: Agents that can follow this one
- `position`: Sequential order in workflow

**Benefit**: Tools can now automatically orchestrate agent workflows

### 2. Input/Output Contracts
```yaml
inputContract:
  required: [approvedPlan]
  optional: [existingCode, testData]
  format: "plan.md from Plan Agent"
  examples: ["# Implementation Plan: ..."]

outputContract:
  primary: code
  secondary: [unitTests, documentation]
  format: "Production code + tests + docs"
```

**Benefit**: Clear validation of agent inputs/outputs; enables contract-based testing

### 3. Tool Capabilities
```yaml
tools:
  - name: editFiles
    capability: write
    requiredFor:
      - generatingProductionCode
      - creatingUnitTests
```

**Benefit**: More precise tool usage tracking and validation

### 4. Explicit Constraints & Rules
```yaml
constraints:
  maxExecutionTime: 1800
  costEstimate: "5000-15000 tokens"
  approvalRequired: false

rules:
  - condition: "plan is ambiguous"
    action: "STOP and request clarification"
    severity: "error"
```

**Benefit**: Runtime validation and execution guarantees

### 5. Enhanced Documentation
- Failure modes with recovery strategies
- Examples with actual input/output samples
- Clear dependency documentation
- Validation checklists for completeness

**Benefit**: Better troubleshooting and onboarding

## Migration Path

### Step 1: Already Applied ✅
- Updated all 5 agents with v2.0 schema
- Enhanced markdown content with new sections
- Added examples and validation checklists

### Step 2: Update Skills (Optional)
Apply the same v2.0 schema to skills:
- `apiVersion: "2.0"`
- `kind: Skill`
- `skillCategory` field
- Input/output contracts
- Usage examples

### Step 3: Create Validation Tooling (Future)
```json
{
  "tools": [
    "agent-spec-validator (validates YAML against schema)",
    "agent-orchestrator (chains agents automatically)",
    "contract-validator (checks input/output contracts)"
  ]
}
```

### Step 4: Document Workflows (Provided)
- Reviewed [AGENTS.md](AGENTS.md) for workflow definitions
- Reviewed [SPEC-SCHEMA.md](SPEC-SCHEMA.md) for schema details

## Backward Compatibility

The modernized agents **maintain full backward compatibility**:
- Existing agent names unchanged
- Same ROLE and core responsibilities
- Same tool usage and patterns
- Markdown content enhanced, not replaced

**Current invocation still works**:
```
@CodingAgent
[Paste approved plan.md]
```

## Using Modernized Agents

### Workflow Execution
```
1. @RequirementAgent → Story (approval needed)
2. @PlanAgent → Plan (approval needed)
3. @CodingAgent + @TestingAgent (parallel)
4. @CommitAgent → Git commit
```

### Validation
New agents validate inputs automatically:
- Requirements Agent: Project context required
- Plan Agent: Approved story required
- Coding Agent: Approved plan required
- Testing Agent: Story with Gherkin required
- Commit Agent: Tests must pass

### Error Handling
Agents now explicitly stop on errors:
```
Plan is ambiguous → STOP and request clarification
Tests failing → STOP before commit
Security issues → STOP and request fix
```

## Best Practices with v2.0

### 1. Always Provide Full Context
- Requirements Agent: Include project documentation
- Plan Agent: Provide story from Requirements Agent
- Coding Agent: Provide approved plan

### 2. Follow Workflow Order
- Don't skip steps (Requirements → Plan → Code)
- Approvals are explicit (wait for signal)
- Parallel execution only for Testing + Coding

### 3. Use Input Contracts
- Check required inputs before invoking
- Provide optional inputs when available
- Use specified formats (e.g., Gherkin for stories)

### 4. Review Output Contracts
- Know what to expect (format, structure, constraints)
- Validate outputs match contract
- Use examples as reference

### 5. Respect Constraints
- Approval required gates (story, plan)
- Token limits (500 for plans, 16K for code)
- Timeouts (10 min for planning, 30 min for coding)

## Troubleshooting

### Agent stops with "ambiguous plan"
**Cause**: Plan lacks clarity or detail
**Solution**: Use Plan Agent to create more detailed plan

### Agent says "tests must pass"
**Cause**: Test suite has failures
**Solution**: Fix failing tests before commit

### Agent outputs exceed token limit
**Cause**: Request too large
**Solution**: Break into smaller, focused tasks

### Agent skips approval gate
**Cause**: Approval not explicitly given
**Solution**: Always approve story/plan before next step

## Structure Going Forward

### Adding New Agents
Use the v2.0 template in [SPEC-SCHEMA.md](SPEC-SCHEMA.md):
1. Define apiVersion, kind, name
2. Add input/output contracts
3. List requirements and rules
4. Provide examples
5. Document composition (chains, position)

### Extending Existing Agents
Update the YAML frontmatter:
1. Increment version number
2. Update constraints/rules as needed
3. Add new examples
4. Document breaking changes

### Creating New Skills
Follow skill-specific extensions:
1. Set `kind: Skill`
2. Define `skillCategory`
3. Add reusability metadata
4. Document usage patterns

## References

- [SPEC-SCHEMA.md](SPEC-SCHEMA.md) - Complete schema definition
- [AGENTS.md](AGENTS.md) - Workflow orchestration guide
- Individual agent files - Implementation examples

## Support

For questions about:
- **Schema**: See [SPEC-SCHEMA.md](SPEC-SCHEMA.md)
- **Workflows**: See [AGENTS.md](AGENTS.md)
- **Specific agents**: See individual agent .md files
- **Skills**: Check .github/skills/ folder
