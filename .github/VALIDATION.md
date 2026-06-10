# Agent Spec v2.0 - Validation & Best Practices

## Overview

This guide helps maintain the spec-driven agent model while leveraging modernization improvements.

## Specification Validation Checklist

### Before Publishing an Agent Spec

#### YAML Frontmatter ✓
- [ ] `apiVersion: "2.0"` present
- [ ] `kind: Agent` or `kind: Skill` specified
- [ ] `name` is unique and kebab-case
- [ ] `title` is human-readable
- [ ] `description` is 1-2 sentences
- [ ] `version` follows semantic versioning
- [ ] `model` specified (for agents)
- [ ] `temperature` within 0.0-1.0
- [ ] `maxTokens` is realistic
- [ ] `timeout` is in seconds

#### Contracts ✓
- [ ] `inputContract.required` lists all required inputs
- [ ] `inputContract.format` specifies expected format
- [ ] `inputContract.examples` provided
- [ ] `outputContract.primary` defined
- [ ] `outputContract.format` describes structure
- [ ] `outputContract.examples` provided

#### Composition ✓
- [ ] `requires` lists all skills/dependencies
- [ ] `chainedWith` lists agents that follow (for agents)
- [ ] `position` specified (for agents)
- [ ] `tools` list all capabilities used

#### Rules & Constraints ✓
- [ ] At least 3 rules defined
- [ ] Each rule has `condition`, `action`, `severity`
- [ ] Constraints include `maxExecutionTime`
- [ ] Error conditions have `severity: "error"`
- [ ] Warnings have `severity: "warning"`

#### Markdown Content ✓
- [ ] ROLE section is clear and concise
- [ ] Responsibilities section is explicit
- [ ] Input Requirements section documented
- [ ] Processing Rules section step-by-step
- [ ] Output Format section detailed
- [ ] Validation Checklist provided
- [ ] Failure Modes with recovery steps
- [ ] Dependencies documented
- [ ] At least 1 concrete example

### Markdown Section Requirements

```markdown
# ROLE
[Clear statement of purpose - 1-2 sentences]

## Responsibilities
- ✓ [What it does]
- ✓ [What it handles]
- ✗ [What it doesn't do]

## Input Requirements
[What information is needed]

## Processing Rules
[Step-by-step approach]

## Output Format
[Deliverable structure]

## Validation Checklist
✓ [Item to verify]

## Failure Modes
| Issue | Recovery |

## Dependencies
[Skills, agents, standards required]

## Examples
[Concrete execution examples]
```

## Composition Rules

### Agent Position Numbering
```
Requirement Agent    → Position 1 (entry point)
Plan Agent          → Position 2 (after requirements)
Coding Agent        → Position 3 (execution)
Testing Agent       → Position 3 (parallel with coding)
Commit Agent        → Position 4 (finalization)
```

### Chaining Patterns

**Valid Chains**:
```
Requirement → Plan → Coding → Commit
Requirement → Plan → Testing → Commit
Story (existing) → Plan → Coding
Plan (existing) → Coding → Commit
```

**Invalid Chains**:
```
X Coding → Requirement (wrong order)
X Plan → Testing → Coding (wrong order)
X Skip Requirement (missing context)
```

### Requirement Specification

When specifying `requires`, use:
- Agent names: `requirement-agent`, `plan-agent`
- Skill paths: `dotnet/api`, `angular/ui`, `automation/playwright`
- Shared skills: `shared/planning`, `shared/gherkin`

## Input/Output Contract Patterns

### Requirement Agent
```yaml
inputContract:
  required: [businessRequirement, projectContext]
  optional: [existingStories, referenceDocumentation]
  format: "Natural language description"

outputContract:
  primary: story
  format: "Markdown with Title, Description, Acceptance Criteria, Gherkin"
```

### Plan Agent
```yaml
inputContract:
  required: [story]
  optional: [existingArchitecture, codebaseContext]
  format: "Approved story document"

outputContract:
  primary: plan
  format: "Structured analysis with Summary, Approach, Risks, Execution Order"
  maxTokens: 500
```

### Coding Agent
```yaml
inputContract:
  required: [approvedPlan]
  optional: [existingCode, testData]
  format: "plan.md from Plan Agent (must be approved)"

outputContract:
  primary: code
  secondary: [unitTests, documentation]
  format: "Production code + tests + docs"
```

## Rule Specification Patterns

### Boundary Rules (stop execution)
```yaml
rules:
  - condition: "plan is ambiguous or incomplete"
    action: "STOP and request clarification"
    severity: "error"
```

### Quality Rules (enforce standards)
```yaml
rules:
  - condition: "code generated"
    action: "Follow existing code patterns and style"
    severity: "error"
```

### Constraint Rules (limits)
```yaml
rules:
  - condition: "story exceeds 800 tokens"
    action: "Recommend splitting into multiple stories"
    severity: "warning"
```

### Validation Rules (before completion)
```yaml
rules:
  - condition: "unit tests created"
    action: "Ensure 80%+ code coverage"
    severity: "error"
```

## Example Specifications

### Complete Agent Spec
See individual agent files:
- [coding-agent.md](agents/coding.agent.md)
- [requirement-agent.md](agents/requirement-agent.md)
- [plan-agent.md](agents/plan-agent.md)

### Skill Template
```yaml
---
apiVersion: "2.0"
kind: Skill
name: skill-name
title: Skill Title
description: Brief description
version: "1.0.0"
skillCategory: "category"
reusableBy: [agent1, agent2]

inputContract:
  context: "What context is assumed"
  examples: ["Example input"]

outputContract:
  guidance: "Type of guidance provided"
  examples: ["Example output"]

examples:
  - scenario: "Use case description"
    input: "Example input"
    expectedOutput: "Example output"
---

# Skill Content
[Detailed guidance and patterns]
```

## Maintenance Guidelines

### When to Update Agent Specs

**Minor version bump** (e.g., v2.0.0 → v2.0.1):
- Add examples
- Clarify documentation
- Update tool capabilities list
- Refine failure modes

**Patch version bump** (e.g., v2.0.0 → v2.1.0):
- Add new optional input/output
- Expand rules set
- Modify constraints
- Change model or temperature

**Major version bump** (e.g., v2.0.0 → v3.0.0):
- Change agent responsibility
- Remove required inputs
- Alter output format
- Change agent position in workflow

### Version Update Process
1. Update version in YAML frontmatter
2. Document changes in Comments section
3. Update examples if behavior changed
4. Test with actual workflow
5. Update MIGRATION.md if breaking changes

### Documentation Review

Review agent spec for:
- [ ] Clear ROLE statement (1-2 sentences)
- [ ] No jargon without explanation
- [ ] Examples are complete and realistic
- [ ] Failure modes have recovery steps
- [ ] Rules are unambiguous
- [ ] Dependencies are listed
- [ ] Markdown is properly formatted

## Performance Optimization

### Token Management

**Requirement Agent**:
- Target: 2000-3000 tokens max
- Strategy: Ask clarifying questions early
- Optimization: Reference existing docs, don't summarize

**Plan Agent**:
- Target: 2000-3000 tokens (output ≤ 500)
- Strategy: Use existing patterns, reference code sections
- Optimization: Don't include code examples in plan

**Coding Agent**:
- Target: 5000-15000 tokens
- Strategy: Execute plan directly, don't re-plan
- Optimization: Use skill templates, follow patterns

**Testing Agent**:
- Target: 4000-8000 tokens
- Strategy: Map to Gherkin scenarios, reuse page objects
- Optimization: Create fixtures, don't duplicate tests

**Commit Agent**:
- Target: 2000-4000 tokens
- Strategy: Review only changed files
- Optimization: Use summary view, focus on deltas

### Execution Time Optimization

**Requirement Agent**: 10 minutes max (may need stakeholder input)
**Plan Agent**: 10 minutes max
**Coding Agent**: 30 minutes max
**Testing Agent**: 20 minutes max
**Commit Agent**: 5 minutes max

## Security & Compliance

### Agent Responsibilities
- Requirement Agent: Never invent business rules
- Plan Agent: No code generation
- Coding Agent: No research phase
- Testing Agent: Maps to requirements
- Commit Agent: Verify security standards

### Data Classification
All agents currently: `Internal` classification
- No public-facing APIs
- All changes internal to project
- Code changes are internal

### Audit Trail
Via YAML spec:
- Agent version tracks changes
- Rules encode compliance requirements
- Tool usage tracked in specification

## Monitoring & Metrics

### Success Indicators
- ✓ Agents execute in sequence
- ✓ Input contracts validated
- ✓ Output contracts met
- ✓ All rules enforced
- ✓ Constraints respected
- ✓ Tokens within budget
- ✓ Execution time within timeout

### Troubleshooting
- Agent stops → Check rules and constraints
- Output incomplete → Verify output contract
- Token overflow → Optimize inputs, break into steps
- Execution timeout → Check complexity, consider splitting

## References

- [SPEC-SCHEMA.md](SPEC-SCHEMA.md) - Complete schema
- [AGENTS.md](AGENTS.md) - Workflow orchestration
- [MIGRATION.md](MIGRATION.md) - Upgrade guide
