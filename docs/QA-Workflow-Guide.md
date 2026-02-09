# QA Workflow Guide: Using Claude Code for Manual Testing

This guide helps QA team members use Claude Code effectively to streamline manual testing, bug reporting, test case creation, and more.

## Table of Contents
1. [Getting Started](#getting-started)
2. [Daily QA Workflows](#daily-qa-workflows)
3. [Common QA Tasks](#common-qa-tasks)
4. [API Testing](#api-testing)
5. [Test Data Generation](#test-data-generation)
6. [Bug Reporting](#bug-reporting)
7. [Test Case Creation](#test-case-creation)
8. [Tips and Best Practices](#tips-and-best-practices)

---

## Getting Started

### What is Claude Code?

Claude Code is an AI-powered CLI tool that helps you with testing tasks. Think of it as a technical assistant that can:
- Generate test cases from requirements
- Create test data
- Help write bug reports
- Generate API test scripts
- Analyze logs and errors
- Create testing documentation

### Installation

```bash
npm install -g @anthropic/claude-code
```

### Starting a Session

```bash
cd /c/github/CompanyApi
claude
```

You'll see a prompt where you can type your requests in natural language.

---

## Daily QA Workflows

### Morning: Check What Changed

**Scenario**: You want to know what was merged yesterday to plan your regression testing.

**What to say**:
```
"Show me all commits from the last 24 hours and list what features/fixes I should test"
```

**What Claude does**:
- Reads git log
- Identifies changed files
- Summarizes what needs regression testing
- Suggests test scenarios

---

### During Testing: API Testing

**Scenario**: You need to test all CRUD operations for the Company API.

**What to say**:
```
"Help me test the Company API. Generate curl commands to:
1. Create a company
2. Get the company by ID
3. Update the company
4. Search for companies
5. Delete the company

Include test data and show me the expected responses."
```

**What Claude does**:
- Reads the API controller code
- Generates ready-to-run curl commands
- Creates realistic test data
- Shows expected HTTP status codes and response formats

**Example output**:
```bash
# 1. Create a company
curl -X POST http://localhost:5139/api/companies \
  -H "Content-Type: application/json" \
  -d '{
    "name": "Acme Corp",
    "industry": "Technology",
    "email": "contact@acme.com",
    "phone": "+1-555-0123"
  }'
# Expected: 201 Created with company ID

# 2. Get company by ID (replace {id} with actual ID from step 1)
curl -X GET http://localhost:5139/api/companies/{id}
# Expected: 200 OK with company details
```

---

### During Testing: Exploratory Testing

**Scenario**: You're testing a new feature and want to think through edge cases.

**What to say**:
```
"I'm testing the company search feature. Create an exploratory testing checklist
covering:
- Edge cases
- Security concerns
- Performance scenarios
- Usability issues
- Data validation"
```

**What Claude does**:
- Analyzes the search implementation
- Generates comprehensive checklist
- Suggests specific test scenarios
- Identifies potential risks

---

### Found a Bug: Writing Bug Reports

**Scenario**: Search crashes when you use special characters.

**What to say**:
```
"Help me write a bug report. When I search for companies with '&' in the name,
I get a 500 error. The API logs show: [paste error from logs]"
```

**What Claude does**:
- Asks clarifying questions (browser, environment, steps to reproduce)
- Analyzes the error
- Generates a detailed bug report with:
  - Title
  - Severity
  - Steps to reproduce
  - Expected vs actual behavior
  - Technical details
  - Suggested priority

---

### End of Day: Test Summary

**Scenario**: You want to summarize your testing session.

**What to say**:
```
"Create a test summary report from my testing today. I tested:
- Company CRUD operations (all passed)
- Search with pagination (found bug with page 0)
- Export feature (all passed)
- API performance with 10k records (slow, needs investigation)"
```

**What Claude does**:
- Formats into professional test report
- Categorizes results (passed/failed/blocked)
- Highlights critical issues
- Suggests next steps

---

## Common QA Tasks

### 1. Generate Test Cases from User Stories

**Task**: You have a user story and need to write test cases.

**Example**:
```
"Read the issue description from GitHub issue #45 and generate test cases including:
- Happy path scenarios
- Negative test cases
- Edge cases
- Boundary conditions"
```

**Alternative** (if you have requirements in a file):
```
"Read docs/requirements/company-export.md and generate detailed test cases"
```

---

### 2. Create Test Data

**Task**: You need realistic test data for manual testing.

**Example**:
```
"Generate 20 company records in JSON format with:
- Mix of US and international addresses
- Various industries
- Some with missing optional fields
- Some with special characters in names
- Mix of active and inactive companies

Save to test-data/companies.json"
```

---

### 3. Compare API Responses

**Task**: You want to verify if an API response changed after a code update.

**Example**:
```
"I saved the API response before the update in old-response.json.
Get the current response from GET /api/companies/123 and compare them.
Show me what changed."
```

---

### 4. Analyze Error Logs

**Task**: The API crashed and you have logs.

**Example**:
```
"Read logs/error.log and:
1. Identify the root cause
2. List all errors in order of occurrence
3. Suggest what I should test to reproduce it
4. Recommend what information to include in the bug report"
```

---

### 5. Create Testing Checklists

**Task**: You want a checklist for regression testing before release.

**Example**:
```
"Create a regression testing checklist for the Company API covering:
- All CRUD operations
- Search and filtering
- Pagination
- Error handling
- Performance
- Security (SQL injection, XSS, etc.)

Format as markdown checklist I can copy-paste."
```

---

### 6. Validate API Contract

**Task**: Ensure API responses match the expected schema.

**Example**:
```
"Check if the Company API responses match the DTOs defined in
src/CompanyApi.Application/Companies/DTOs/. Test all endpoints and report any mismatches."
```

---

## API Testing

### Quick API Health Check

```
"Is the Company API running? Check the health endpoint and test one CRUD operation
to verify it's working."
```

### Test All Endpoints Systematically

```
"Test all Company API endpoints in sequence:
1. POST /api/companies (create)
2. GET /api/companies/{id} (read)
3. GET /api/companies (list all)
4. GET /api/companies?pageNumber=2&pageSize=10 (pagination)
5. GET /api/companies/search?searchTerm=tech (search)
6. PUT /api/companies/{id} (update)
7. DELETE /api/companies/{id} (delete)

For each, show me the curl command, execute it, and verify the response."
```

### Test Error Scenarios

```
"Test error handling for the Company API:
- Create company with missing required fields
- Get company with invalid ID
- Update company that doesn't exist
- Create company with duplicate data
- Send malformed JSON
- Send requests with extremely large payloads

Show me the curl commands and verify we get appropriate error codes and messages."
```

### Performance Testing

```
"Help me do basic performance testing:
1. Create 1000 companies using the API
2. Time how long it takes to search through them
3. Test pagination with 100 items per page
4. Report the results"
```

---

## Test Data Generation

### Basic Test Data

```
"Generate 10 companies in JSON format suitable for POST /api/companies"
```

### Complex Test Scenarios

```
"Generate test data covering these scenarios:
1. Company with minimum required fields only
2. Company with all fields populated
3. Company with very long name (255 characters)
4. Company with international characters (Japanese, Arabic, Emoji)
5. Company with special characters (&, <, >, ', \")
6. Company with SQL injection attempt in name field
7. Company with XSS attempt in description
8. Company with future founded date
9. Company with founded date from 1800
10. Company with negative employee count

Save each as separate JSON files in test-data/"
```

### Database Seeding

```
"Create a script that seeds the MongoDB database with 100 realistic companies
covering various industries, locations, and sizes. Include companies that:
- Have been active for different durations
- Have different employee counts (1 to 50000)
- Are from at least 10 different countries
- Cover 20 different industries"
```

---

## Bug Reporting

### Structured Bug Report

**What to say**:
```
"Help me write a bug report:

Issue: When I search for companies, pagination breaks after page 5
Steps I took: [describe what you did]
Expected: Should show page 6 results
Actual: Returns 500 error
Environment: Development API running locally
Logs: [paste relevant logs]"
```

**Claude creates**:
```markdown
# Bug Report: Pagination Fails After Page 5

**Severity**: High
**Priority**: P1
**Component**: Company Search API
**Environment**: Development (localhost:5139)

## Description
The search API returns a 500 Internal Server Error when requesting page 6 or higher results.

## Steps to Reproduce
1. Start the API locally
2. Create 100+ companies in the database
3. Execute: GET /api/companies/search?searchTerm=corp&pageNumber=6&pageSize=10
4. Observe 500 error response

## Expected Behavior
API should return page 6 results with 10 companies (or fewer if less available)

## Actual Behavior
API returns 500 error with message: "Sequence contains no elements"

## Technical Details
- Endpoint: GET /api/companies/search
- Status Code: 500
- Error: System.InvalidOperationException: Sequence contains no elements
- Stack trace: [from logs]

## Additional Context
- Pages 1-5 work correctly
- Issue occurs with any search term
- Same behavior in both search and list-all endpoints

## Suggested Priority
P1 - Blocks testing of pagination feature for release

## Attachments
- error-logs.txt
- screenshot-500-error.png
```

---

### Quick Bug Format

For minor bugs, use shorthand:

```
"Quick bug report: Company update API doesn't validate email format.
I can save 'notanemail' as email and it accepts it."
```

---

## Test Case Creation

### From User Story

```
"Read this user story and create detailed test cases:

As a user, I want to export company data to CSV so that I can analyze it in Excel.

Acceptance Criteria:
- Export button on company list page
- Downloads CSV file with all company fields
- File name includes current date
- Works with filtered/searched results"
```

### From Code

```
"Read the CompanyController.cs and generate test cases for all endpoints.
Include positive, negative, and edge case scenarios for each."
```

### Regression Test Suite

```
"Create a complete regression test suite for the Company API that covers:
- All CRUD operations
- All query operations (search, filter, pagination)
- Error handling for each endpoint
- Performance acceptance criteria
- Security test cases

Format as a spreadsheet-ready CSV with columns:
Test ID, Test Case, Steps, Expected Result, Priority"
```

---

## Tips and Best Practices

### 1. Be Specific

**Instead of**:
```
"Help me test the API"
```

**Say**:
```
"Test the Company search API with these scenarios: empty search term,
special characters, very long search term, and verify pagination works correctly"
```

### 2. Provide Context

**Instead of**:
```
"I found a bug"
```

**Say**:
```
"I found a bug in the search feature. When I search for 'test & demo',
I get a 500 error. Here are the logs: [paste logs].
This happens on localhost:5139 running the latest master branch."
```

### 3. Ask for Explanations

```
"Explain what this error means and why it might be happening:
MongoDB.Driver.MongoCommandException: Command find failed: invalid query"
```

### 4. Request Formats That Help You

```
"Generate test cases as a markdown checklist so I can check them off as I test"
```

Or:
```
"Generate test data as CSV so I can import it into our test management tool"
```

### 5. Combine Multiple Tasks

```
"Do the following:
1. Generate 50 test companies
2. Create curl commands to insert them via API
3. Create curl commands to search for various terms
4. Create a bash script that runs all of this and logs results"
```

### 6. Learn from Code

```
"Read the validation logic in CreateCompanyCommandValidator.cs and explain
what validation rules I need to test"
```

### 7. Reuse Previous Work

```
"Yesterday you generated API test curl commands. Update those to test the new
'tags' field that was added to the Company entity"
```

### 8. Automate Repetitive Tasks

```
"I need to test the same search scenarios after every code change.
Create a bash script I can run that:
- Tests 10 different search terms
- Verifies expected counts
- Logs pass/fail for each
- Outputs a summary report"
```

### 9. Verify Fixes

```
"Bug #123 was marked as fixed. Read the bug description and the fix commit,
then generate test cases to verify it's actually fixed and won't regress."
```

### 10. Cross-Reference Documentation

```
"Compare the API documentation in README.md with the actual controller endpoints
and tell me if there are any discrepancies"
```

---

## Example: Complete Testing Session

Here's a real example of how a QA might use Claude Code for a full testing session:

### Step 1: Morning Standup Prep
```
User: "What PRs were merged yesterday? List the changes and what I should test."

Claude: [Lists 3 PRs with summaries]
- PR #5: Added company tags feature → Test CRUD with tags
- PR #6: Fixed pagination bug → Regression test pagination
- PR #7: Updated validation → Test validation edge cases
```

### Step 2: Start Testing New Feature
```
User: "Create test cases for the new tags feature. Read the code changes
in PR #5 to understand what it does."

Claude: [Generates 15 test cases covering CRUD operations with tags,
edge cases like empty tags, duplicate tags, special characters, etc.]
```

### Step 3: Manual Testing
```
User: "Generate curl commands to test all the tag scenarios you listed.
Use realistic test data."

Claude: [Creates 15 curl commands with JSON payloads ready to copy-paste]
```

### Step 4: Found a Bug
```
User: "When I send an empty tags array, the API returns 400 but should
accept it. Help me write a bug report."

Claude: [Generates detailed bug report with severity, repro steps, logs analysis]
```

### Step 5: Regression Testing
```
User: "The pagination fix was supposed to solve the 'page 6' issue.
Create a test script that verifies pages 1-20 all work correctly."

Claude: [Creates bash script that creates 200 test records, queries each page,
and logs results]
```

### Step 6: Performance Check
```
User: "Test how the search API performs with 10,000 companies.
Time the responses and tell me if any queries take >2 seconds."

Claude: [Seeds database, runs timed searches, reports results]
```

### Step 7: End of Day Summary
```
User: "Create a test summary report. I tested:
- Tags CRUD (found 1 bug with empty arrays)
- Pagination regression (all passed)
- Search performance (2 queries were slow with 10k records)
Include the bug report and performance data."

Claude: [Generates formatted test summary report ready to share with the team]
```

---

## Getting Help

If you're stuck, just ask Claude:

```
"I want to test X but I'm not sure how to start. What should I do?"
```

Or:

```
"What are the most important test scenarios for feature X?"
```

Or:

```
"Explain how feature X works so I can understand what to test"
```

---

## Advanced: Creating Reusable Test Scripts

Once Claude generates scripts you find useful, save them for reuse:

```
User: "Create a script called qa-smoke-test.sh that runs a quick smoke test
of all Company API endpoints. I want to run this before every release."

Claude: [Creates reusable script]

User: "Add that script to the repository in /scripts folder and update
the README with instructions on how QA should use it."
```

Now your whole team can use that script!

---

## Questions?

If you have questions about using Claude Code for QA tasks, try:

```
"I want to [describe your goal]. How can I do this with Claude Code?"
```

Claude will guide you through the process or create tools/scripts to help you accomplish it.

Happy testing!
