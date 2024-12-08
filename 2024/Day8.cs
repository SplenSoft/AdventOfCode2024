﻿using System.Numerics;

namespace AdventOfCode._2024;

/// <summary>
/// <see href="https://adventofcode.com/2024/day/8"/>
/// </summary>
[Day(2024, 8)]
internal class Day8 : Day
{
    public override string Input => Resources._2024_8_Input;

    public override async Task<string> Solve(string input)
    {
        string[] lines = input.Split(Environment.NewLine);
        List<List<Vector2>> nodes = [[/*part1*/], [/*part2*/]];
        List<(char c, Vector2 v)> ants = [..lines.SelectMany((y, i) => y
            .Select((x, j) => (x, new Vector2(j, i)))).Where(x => x.x != '.')];

        ants.ForEach(a1 => ants
            .Where(a2 => a1 != a2 && a1.c == a2.c).ToList().ForEach(a2 =>
            {
                var antiNode = a1.v + (a1.v - a2.v);
                nodes[0].Add(antiNode);
                nodes[1] = [.. nodes[1], antiNode, a1.v];

                while (IsInBounds(antiNode))
                    nodes[1].Add(antiNode += a1.v - a2.v);
            }));

        bool IsInBounds(Vector2 pos)=> pos.X >= 0 && pos.X < lines[0].Length 
            && pos.Y >= 0 && pos.Y < lines.Length;

        return $"Part 1: {nodes[0].Distinct().Where(IsInBounds).Count()}" +
            $"\nPart 2: {nodes[1].Distinct().Where(IsInBounds).Count()}";
    }
}