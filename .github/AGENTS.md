# Agent Workflow Orchestration

## Workflow Diagram

```
User Request
    ↓
[Requirement Agent] → Story Document
    ↓
[Plan Agent] → Implementation Plan
    ↓ (approval)
[Coding Agent] → Production Code
    ↓ (parallel)
    ├→ [Testing Agent] → Test Suite
    └→ [Code Review] → Review Feedback
    ↓ (integration)
[Commit Agent] → Git Commit
    ↓
Deployed Solution
```

## Agent Specifications

### 1. Requirement Agent
**Position**: 1st (entry point)
**Input**: Business requirement or feature description
**Output**: Implementation-ready story with acceptance criteria
**Dependencies**: project documentation, existing stories, API contracts

### 2. Plan Agent  
**Position**: 2nd (after requirements)
**Input**: Approved story document
**Output**: Structured implementation plan
**Dependencies**: shared/planning, shared/token-optimization
**Chains To**: Coding Agent

### 3. Coding Agent
**Position**: 3rd (execution)
**Input**: Approved implementation plan
**Output**: Production code + unit tests + documentation
**Dependencies**: dotnet/api, dotnet/unit-test, angular/ui, angular/unit-test
**Chains To**: Testing Agent, Commit Agent

### 4. Testing Agent
**Position**: 3rd-parallel (during coding)
**Input**: Story + approved plan
**Output**: Automated test suite with test data & scenarios
**Dependencies**: automation/playwright
**Chains To**: Commit Agent

### 5. Commit Agent
**Position**: 4th (finalization)
**Input**: Code changes + test results
**Output**: Conventional commit via Git
**Dependencies**: code-review/review, commit/conventional
**Final Agent**: Yes (no chains after)

## Workflow Rules

- **Sequential**: Requirement → Plan → Coding
- **Parallel**: Testing can run alongside Coding
- **Approval Gates**: 
  - Story requires stakeholder review
  - Plan requires approval before Coding
  - Tests must pass before Commit
- **Error Handling**: Stop on ambiguous input, ask for clarification
- **Token Optimization**: Each agent respects max token limits

## Agent Chaining Examples

### Feature Development
```
Requirement Agent → Plan Agent → Coding Agent + Testing Agent → Commit Agent
```

### Refinement Only
```
Plan Agent → Coding Agent → Commit Agent
```

### Testing Enhancement
```
Story (existing) → Testing Agent → Commit Agent
```

## Invocation Patterns

### Direct Call
User invokes specific agent with input:
```
@PlanAgent
Create a plan for: [story text]
```

### Workflow Call
User invokes workflow entry point:
```
@RequirementAgent
Add this feature: [business description]
```

### Skip to Phase
User provides prerequisite artifacts:
```
@CodingAgent
[Paste approved plan.md]
```

## Configuration

- **Default Model**: claude-3-5-sonnet (Coding), claude-3-sonnet (others)
- **Default Timeout**: 30 minutes (Coding), 10 minutes (others)
- **Max Tokens**: 8000 (planning), 16000 (coding), 4000 (others)
- **Temperature**: 0.3 (deterministic), 0.7 (creative)
