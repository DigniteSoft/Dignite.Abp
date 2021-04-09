# Contribution Guide

Dignite is a community driven open source project. This guide aims to help anyone who wants to contribute to the project.

## <a name="coc"></a> Code of Conduct

Dignite works on making a open and inclusive community , read this copy of [Contributor Covenant](CODE_OF_CONDUCT.md) to help us keep it.

## <a name="question"></a> Got a question or problem?

Despite you can open issues for such things, we recommend using [discussion](https://github.com/digniteplatform/dignite/discussion) to ask support-related questions since we want to keep issue lists clean.

If you would like to chat about the question in real-time, you can reach out via our [Gitter] server.

## <a name="issue"></a> Found a Bug?

If you find a bug in the source code, you can help us by [submitting an issue](#submit-issue) to our [GitHub Repository](https://github.com/digniteplatform/dignite).
Even better, you can [submit a Pull Request](#submit-pr) with a fix.

## <a name="feature"></a> Missing a Feature?

You can *request* a new feature by [submitting an issue](#submit-issue) to our GitHub Repository.
If you would like to *implement* a new feature, please consider the size of the change in order to determine the right steps to proceed:

* For a **Major Feature**, first open an issue and outline your proposal so that it can be discussed.
  This process allows us to better coordinate our efforts, prevent duplication of work, and help you to craft the change so that it is successfully accepted into the project.

  **Note**: Adding a new topic to the documentation, or significantly re-writing a topic, counts as a major feature.

* **Small Features** can be crafted and directly [submitted as a Pull Request](#submit-pr).


## <a name="submit"></a> Submission Guidelines

### <a name="submit-issue"></a> Submitting an Issue

Before you submit an issue, please search the [issue tracker](https://github.com/digniteplatform/dignite/issues), maybe an issue for your problem already exists and the discussion might inform you of workarounds readily available.

We want to fix all the issues as soon as possible, but before fixing a bug we need to reproduce and confirm it.
In order to reproduce bugs, we require that you provide a minimal reproduction.
Having a minimal reproducible scenario gives us a wealth of important information without going back and forth to you with additional questions.

A minimal reproduction allows us to quickly confirm a bug (or point out a coding problem) as well as confirm that we are fixing the right problem.

We require a minimal reproduction to save maintainers' time and ultimately be able to fix more bugs.
Often, developers find coding problems themselves while preparing a minimal reproduction.
We understand that sometimes it might be hard to extract essential bits of code from a larger codebase but we really need to isolate the problem before we can fix it.

Unfortunately, we are not able to investigate / fix bugs without a minimal reproduction, so if we don't hear back from you, we are going to close an issue that doesn't have enough info to be reproduced.

### <a name="submit-pr"></a> Submitting a Pull Request (PR)

Before you submit your Pull Request (PR) consider the following guidelines:

1. Search [PR Lists](https://github.com/digniteplatform/dignite/pulls) for an open or closed PR that relates to your submission.
   You don't want to duplicate existing efforts.

2. Be sure that an issue describes the problem you're fixing, or documents the design for the feature you'd like to add.
   Discussing the design upfront helps to ensure that we're ready to accept your work.

3. [Fork](https://docs.github.com/en/github/getting-started-with-github/fork-a-repo) this repo.

4. In your forked repository, make your changes in a new git branch:

   ```shell
   git checkout -b my-fix-branch master
   ```

5. Create your patch, **including unit/integration test cases**.

6. Send a pull request.
