package main

import (
	"context"
	"os/exec"

	"go.temporal.io/sdk/activity"
	"go.temporal.io/sdk/workflow"
)

func InfrastructureProvisionWorkFlow(ctx workflow.Context) error {
	return nil
}

func CreateBucket(ctx context.Context, flowId string) (string, error) {

	logger := activity.GetLogger(ctx)
	logger.Info("Activity", "name")

	cmd := exec.Command("terraform", "init")
	_, err := cmd.Output()

	if err != nil {
		return "something went wrong", err
	}

	cmd = exec.Command("terraform", "apply", "-auto-approve")
	_, err = cmd.Output()

	if err != nil {
		logger.Error(err.Error())
	}
	return "infra applied", err
}
