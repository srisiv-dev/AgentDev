# Agent & Skill Specification Schema v2.0

## Overview
This schema defines the standardized format for agents and skills, enabling machine-readable specifications while maintaining human clarity.

## Core Structure

### YAML Frontmatter Metadata
```yaml
---
apiVersion: "2.0"                    # Spec version
kind: "Agent" | "Skill"              # Resource type
name: string                         # Unique identifier
title: string                        # Display name
description: string                 # Brief summary (1-2 sentences)
version: string                      # Semantic version
author: string                       # Creator/maintainer
tags: [string]                       # Category tags

# Execution Configuration
model: string                        # LLM model (e.g., claude-3-5-sonnet)
temperature: number                  # 0.0-1.0 (optional, default 0.7)
maxTokens: number                    # Output limit
timeout: number                      # Execution timeout in seconds

# Composition
inherits: [string]                   # Parent specs to inherit from
requires: [string]                   # Skills/agents this depends on
chainedWith: [string]                # Agents that follow in workflow
canChainTo: [string]                 # Agents that can follow this

# Capabilities
tools:
  - name: string                     # Tool identifier
    capability: string               # What it enables (read|write|analyze|execute)
    requiredFor: [string]             # Required for which responsibilities
  
scope:
  read: [string]                     # What can be read
  write: [string]                    # What can be modified
  execute: [string]                  # What commands can run

# Input/Output Contracts
inputContract:
  required: [string]                 # Required input fields
  optional: [string]                 # Optional input fields
  format: string                     # Format expectations
  examples: [string]                 # Example inputs

outputContract:
  primary: string                    # Main deliverable type
  secondary: [string]                # Additional outputs
  format: string                     # Output format
  examples: [string]                 # Example outputs

# Validation & Constraints
constraints:
  maxExecutionTime: number            # Seconds
  costEstimate: string                # Token/cost estimate
  dataClassification: string          # Public|Internal|Confidential
  approvalRequired: boolean           # Requires review before execution

rules:
  - condition: string                # When rule applies
    action: string                   # What must happen
    severity: "error" | "warning"    # Violation severity

---
```

## Markdown Content Format

### Section Structure
```markdown
# ROLE
[Clear statement of agent/skill purpose and boundaries]

## Responsibilities
- [Specific duty]
- [What it does]
- [What it doesn't do]

## Input Requirements
[What information is needed to execute]

## Processing Rules
[Step-by-step approach, decision points, rules]

## Output Format
[What will be delivered, structure, constraints]

## Validation Checklist
✓ [Item to verify before completion]

## Failure Modes
[What could go wrong and recovery steps]

## Dependencies
[Skills, agents, or external systems required]

## Examples
[Concrete examples of proper execution]
```

## Agent-Specific Extensions

Agents must include:
```yaml
---
kind: Agent
agentType: "workflow" | "execution" | "analysis"
position: number                     # Order in workflow (1=first)

inputContract:
  required:
    - approvedPlan                   # For Coding Agent
    - story                          # For Testing Agent

outputContract:
  primary: "code" | "tests" | "plan" | "story" | "commit"
  
rules:
  - condition: "plan is ambiguous"
    action: "STOP and request clarification"
    severity: "error"
---
```

## Skill-Specific Extensions

Skills must include:
```yaml
---
kind: Skill
skillCategory: string                # e.g., "dotnet", "angular", "automation"
reusableBy: [string]                 # Which agents can use this skill

inputContract:
  context: string                    # What context is assumed

outputContract:
  guidance: string                   # Type of guidance provided
  
examples:
  - scenario: string
    input: string
    expectedOutput: string
---
```

## Validation Rules

All specs must:
1. ✓ Have unique `name` within kind
2. ✓ Define clear `inputContract.required`
3. ✓ Define clear `outputContract`
4. ✓ Include at least one rule or constraint
5. ✓ Document all dependencies
6. ✓ Include failure modes
7. ✓ Have concrete examples

## Benefits

- **Machine-Readable**: Tools can parse and validate specs
- **Composable**: Clear dependencies enable agent chaining
- **Traceable**: All rules and constraints are documented
- **Maintainable**: Schema version enables evolution
- **Reusable**: Skills can be composed and shared
- **Testable**: Input/output contracts enable validation

## Migration Path

1. Update existing agents (see MIGRATION.md)
2. Standardize all agent YAML frontmatter
3. Enhance skill definitions with new schema
4. Create validation tooling
5. Document agent workflows and chaining patterns
