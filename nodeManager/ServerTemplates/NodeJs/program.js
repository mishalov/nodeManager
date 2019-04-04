module.exports.program = function(args) {
  if (!args.array || !args[0] || !args[1]) {
    return `Неверные входные данные!: ${JSON.stringify(args)}`;
  }
  return args[0] + args[1];
};
