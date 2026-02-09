# QA Quick Reference: Claude Code Commands

Print this page and keep it handy! Copy-paste these prompts to get started quickly.

---

## Daily Workflows

### Morning: Check What Changed
```
"Show me all commits from the last 24 hours and list what I should test"
```

### Check if API is Running
```
"Is the Company API running? Test the health endpoint and one CRUD operation"
```

---

## API Testing

### Test All CRUD Operations
```
"Test all Company API CRUD operations with curl commands. Use realistic test data
and show expected responses"
```

### Test Specific Endpoint
```
"Test the company search endpoint with these scenarios: empty search, special
characters, long text, and verify pagination works"
```

### Test Error Handling
```
"Test error scenarios for Company API: missing fields, invalid IDs, malformed JSON,
duplicate data. Show curl commands and verify error responses"
```

---

## Test Data

### Generate Basic Test Data
```
"Generate 10 realistic company records in JSON format"
```

### Generate Edge Case Data
```
"Generate test data with edge cases: empty fields, maximum length, special
characters, international characters, SQL injection attempts"
```

### Seed Database
```
"Create a script to seed MongoDB with 100 realistic companies covering various
industries, countries, and sizes"
```

---

## Bug Reports

### Detailed Bug Report
```
"Help me write a bug report:
Issue: [describe issue]
Steps: [what you did]
Expected: [what should happen]
Actual: [what happened]
Logs: [paste logs]"
```

### Quick Bug Report
```
"Quick bug report: [describe the issue and how to reproduce it]"
```

### Analyze Error
```
"Explain this error and what might be causing it: [paste error message]"
```

---

## Test Cases

### Generate from User Story
```
"Read this user story and create test cases: [paste user story]"
```

### Generate from Code
```
"Read CompanyController.cs and generate test cases covering all endpoints
with positive, negative, and edge cases"
```

### Regression Test Suite
```
"Create a complete regression test suite for the Company API covering CRUD,
search, filtering, error handling, and security"
```

---

## Exploratory Testing

### Create Testing Checklist
```
"Create an exploratory testing checklist for [feature name] covering edge cases,
security, performance, usability, and data validation"
```

### Understand What to Test
```
"Explain how the [feature name] works so I can understand what to test"
```

---

## Analysis

### Compare API Responses
```
"Compare the API response in old-response.json with the current response from
GET /api/companies/123 and show what changed"
```

### Analyze Logs
```
"Read logs/error.log and identify root cause, list errors in order, suggest
how to reproduce, and recommend info for bug report"
```

### Verify Documentation
```
"Compare the API documentation in README.md with actual endpoints and
report any discrepancies"
```

---

## Test Scripts & Automation

### Create Reusable Test Script
```
"Create a bash script called test-company-api.sh that tests all endpoints,
logs results, and outputs a pass/fail summary"
```

### Performance Test
```
"Create 1000 test companies, time search operations, test pagination with 100
items per page, and report performance results"
```

---

## Verification

### Verify Bug Fix
```
"Bug #[number] was marked fixed. Read the bug and the fix commit, then generate
test cases to verify it's fixed and won't regress"
```

### Verify Feature Complete
```
"Read the acceptance criteria for [feature] and generate a checklist to verify
all requirements are met"
```

---

## End of Day

### Test Summary Report
```
"Create a test summary report from today's testing:
- [Feature 1]: [results]
- [Feature 2]: [results]
- [Bug found]: [description]"
```

---

## Tips

1. **Be specific** - Say exactly what you want to test
2. **Provide context** - Include error messages, logs, environment details
3. **Ask for formats** - "as markdown checklist", "as CSV", "as bash script"
4. **Combine tasks** - Ask for multiple things in one prompt
5. **Save scripts** - Keep useful scripts for reuse

---

## Common Questions

### "I don't know where to start"
```
"I need to test [feature/area]. What should I test and how do I start?"
```

### "I'm stuck"
```
"I'm trying to [goal] but [problem]. How can I do this?"
```

### "I need to learn the code"
```
"Explain how [feature] is implemented so I can understand what to test"
```

---

**Full Guide**: See `docs/QA-Workflow-Guide.md` for detailed examples and workflows
