# Drill Bit Sizer

If you have a bucket of drills you need to sort, lots of them might not have markings.

With an imperial caliper, you can measure across the flutes near the tip to find the size in decimal inches.

Type this number at the prompt, and you will get the three closest sizes in both imperial and metric.

## Examples

In this example, we have a good measurement of a letter size `V` drill. You can see the `V` size in the middle, and the 
next size up and down along with the error percentage.
```text
> .377
      3/8" (0.3750, -0.53% error)
         V (0.3770,  0.00% error)
   9.60 mm (0.3780,  0.27% error)
```

In this example, our measurement doesn't exactly match any size, but is probably a `53/64"` drill if you aren't
expecting to find any metric drills. If you do expect metric drills, it's slightly more likely to be a `21 mm` drill.
```text
> .823
    13/16" (0.8125, -1.28% error)
  21.00 mm (0.8268,  0.46% error)
    53/64" (0.8281,  0.62% error)
```

## Build

```shell
dotnet publish -c Release
```